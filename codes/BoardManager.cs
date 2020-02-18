using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { set; get; }
    public Piece[,] pieces = new Piece[14, 14]; // creating a piece array of size 14 * 14 which is the size of our board.
    public GameObject TigerPiecePrefab; // reference for spawnning the Tiger Piece Prefab.
    public GameObject GoatPiecePrefab;  // reference for spawnning the Goat Piece Prefab
    [SerializeField]
    private GameObject gameOverUI; // setting display canvas for gameover situation
    public static int Count = 0; // boolean to change between the scenes.
    public static int money; // switch condition for tiger to check score in quiz.
    public static int cash; // switch condition for goat to check score in quiz.

    private Quaternion orientation = Quaternion.Euler(0, 180, 0); // initializing for tiger rotation by 180 degree.
    private Quaternion orientationGoat = Quaternion.Euler(0, 110, 0); // initializing for goat rotation by 110 degree.
    private Quaternion orientationGoat1 = Quaternion.Euler(0, -110, 0); // initializing for goat rotation by 250 degree.

    [SerializeField] AudioClip Goatmule; // initializing sound Gameobject for Goat movement on engine.
    [SerializeField] AudioClip TigerRoar; // initializing sound Gameobject for Tiger movement on engine.
    [SerializeField] AudioClip GoatDead; // initializing sound Gameobject for Goat kill on engine.

    public GameObject Highlighter; //initializing gameObject for pointing out movable tigers.
    public GameObject lifecube1, lifecube2, lifecube3, lifecube4, lifecube5; // initializing 5 lives of goat which is a cube gameobject
    public GameObject boardTiger; // Generating board for tiger turn.
    public GameObject boardGoat;  // Generating board for goat turn. 

    AudioSource audioSource; // initializing audio sources.
    AudioSource audioSource1;
    AudioSource audioDead;

    public static bool isRedTurn; // for Tiger turn
    public bool isRed; // pin pointing Tiger Gameobject among all Gameobject.
    public static int health; // health count for Goat lives.

    public CanvasGroup ActiveCanvasCaution; // for creating alert canvas with reduced opacity over time.
    private bool ActiveCanvas; // for triggering alert canvas.
    public static bool gameIsOver; // for triggering gameover canvas.
    public static bool goatisdead; // for triggering death animation of goat.

    private int count = 0; // initializing count.
    public Text countText; // initializing for displayed lives count. 
    public Text tigerblockedText; // set count for  freezed tiger Gameobject.
    public Text tigerfreeText; // set count for movable tigers.
   // private int CurrentSceneIndex;
    private float number = 1.0f;
    private int second;
    //private int alok;
    private int tigerblocked = 0; // initializing blocked tiger count.
    private int tigerfree = 0; // initializing movable tiger count.

    private Vector3 Offsetforboard = new Vector3(-5.0f, 0.35f, -2.0f); // relocating the origin of the world to accompany with the board.
    private Vector3 OffsetforPiece = new Vector3(0, -0.05f, 0); // offset to put the pieces not at the level of board but at top of the board.

    public static Vector2 MousePosition; // Position of Cursor at Board
    

    private Piece ChoosenPiece; // parameter for selecting piece.
    private List<Piece> freezedPieces; // creating array for freezed Moves


    private Vector2 initialMove; // to declare the starting move position(from).
    private Vector2 finalMove;   // to declare the final move position(to).

    Animator Animation; //initializing animator.

    private void Start()
    {
        gameIsOver = false; // since at the start, game over is not true.
        audioSource = GetComponent<AudioSource>(); // instantiating audio
        audioSource1 = GetComponent<AudioSource>(); 
        audioDead = GetComponent<AudioSource>(); // instantiating audio for death sound.
        foreach (Transform t in Highlighter.transform)
        {
            t.position = Vector3.down * 100; // when its goat turn keeping the highlight container just below the board and apperas on top of board for tiger turn if valid condition.
        }
        isRedTurn = false; // since Goat starts the first move.
        isRed = false; // de-selecting Tiger Gameobject at start of the game and re-selecting for tiger turn.
        freezedPieces = new List<Piece>(); // initializing freezed pieces.
        GenerateGoat(); // spawnning Goat pieces on the board based on score points.
        GenerateTiger(); // spawnning Tiger pieces on the board based on score points.
    }

    private void Update() // called every frame per unit of time
    {
        if (!PauseMenu.GameIsPaused) // for pause functionality.
        {
            foreach (Transform t in Highlighter.transform)
            {
                t.Rotate(Vector3.up * 90 * Time.deltaTime); // rotating all live highlighter to positive 90 degree every frame per second.
            }

            ReviseCanvasAlert(); // since swapping is updated for every turn change.
            SetCountText(); // updated for each lives of goat.
            UpdateMousePosition(); // called every frame to update raycast position
            CheckWinCondition(); // need to check for win condition every frame.
            life(); 
            TigerBlocked();
        }

        if (!gameIsOver) // raycasting for mouse click operation occurs until game is over.
        {
            if ((isRed) ? isRedTurn : !isRedTurn) //ternary operation to decide among tiger and goat.
            {
                int x = Mathf.RoundToInt(MousePosition.x); // raycast value of x from updatemouseposition function
                int y = Mathf.RoundToInt(MousePosition.y); // raycast value of y from updatemouseposition function

                if (ChoosenPiece != null) // only happens if there is a choosen piece.
                    PieceMoveUpdate(ChoosenPiece); // Piece will move along with the left click of mouse unless released for drag animation.

                if (Input.GetMouseButtonDown(0)) // click event( same as left click pressed)
                {
                    ChoosePiece(x, y); // Select the piece if it is well within the bound and mouse is clicked
                }

                if (Input.GetMouseButtonUp(0)) // mouse release event (same as left click is released)
                {
                    TryMoving(Mathf.RoundToInt(initialMove.x), Mathf.RoundToInt(initialMove.y), x, y); // start dragging piece from its defined initialMove position from choosefunction to its finalMove position
                }
            }
            number += Time.deltaTime;
            second = Mathf.FloorToInt(number);
            if (second == 3) // it is in 3rd running second
            {
                Count++; // increasing Count
                if (Count < 2) // if count is less than 2
                {
                    SceneManager.LoadScene("persistantGoat"); // loading scene for Goat
                }
                else if (Count == 2) 
                {
                    SceneManager.LoadScene("persistant"); //loading scene for tiger.
                }
            }
        }
        
        freezedPieces = CheckForAvailableMove(); // since we are checking it every frame.
        if (isRedTurn)
        {
            if (freezedPieces.Count == 0) // if the list on freezedPiece is zero, means there are no possible moves for tiger.
            {
                EndGame(); // if no move available call the end game function to decide the winner.
                return;
            }
        }
    }

    public void TryMoving(int x1, int y1, int x2, int y2) // for drag from initial drag position to final drag position.
    {
        freezedPieces = CheckForAvailableMove(); // since we are checking it even when other side of the player i.e goat is playing.
        initialMove = new Vector2(x1, y1); // re-defining initialMove and finalMove in case its not the same piece turn.
        finalMove = new Vector2(x2, y2);
        ChoosenPiece = pieces[x1, y1] // re-defining the selected piece in case if its not the same piece turn.
;                                      // choosepiece and initial drag are the same for piece.

        // Out of bounds
        if (x2 < 3 || x2 > 11 || y2 < 0 || y2 > 8)
        {
            if (ChoosenPiece != null)
                MoveGameObject(ChoosenPiece, x1, y1); // if piece is selected but destination position is out of bound then final drag position to be the starting point i.e initial drag

            initialMove = Vector2.zero; // resetting initialMove to zero once moved to its final position.
            ChoosenPiece = null; // selection should be zero after the final move.
            Highlight(); // need not change the highlighter position if move is not sucessful.
            return;
        }

        if (ChoosenPiece != null)
        {
            // If it has not moved
            if (finalMove == initialMove) // if there is a selection but has not moved then in that case we just cancel te move.
            {
                MoveGameObject(ChoosenPiece, x1, y1); //moving back to initial position
                initialMove = Vector2.zero; // resetting start point
                ChoosenPiece = null; // resetting selection.
                Highlight(); // need not change the highlighter position if move is not sucessful.
                return;
            }

            // check if it is a valid move..
            if (ChoosenPiece.MoveRule(pieces, x1, y1, x2, y2))
            {
                // Did we kill anything?
                // If it is a jump with a tiger turn
                if (isRed)
                {
                    if (Mathf.Abs(x2 - x1) == 4 || Mathf.Abs(y2 - y1) == 4) // since jump movement is double than normal jump
                    {
                        Piece p = pieces[(x1 + x2) / 2, (y1 + y2) / 2];
                        if (p != null) // if piece in between is not null and is not a tiger
                        {
                            pieces[(x1 + x2) / 2, (y1 + y2) / 2] = null; // reset the position to null
                            goatisdead = true; // boolen condition for playing death animation
                            audioDead.PlayOneShot(GoatDead); // Goat dead sound 
                            Destroy(p.gameObject, 4); // if condition is true deleting goat Gameobject from the board after 4 seconds.
                        }
                    }
                }

                pieces[x2, y2] = ChoosenPiece; // setting the array since Gameobject has moved.
                pieces[x1, y1] = null;  // setting initial position of the Gameobject to none since it has moved
                MoveGameObject(ChoosenPiece, x2, y2);  // move operation.          
                SwitchTurn(); // since turn has ended for the Gameobject.
            }
            else
            {
                MoveGameObject(ChoosenPiece, x1, y1); // if move is not successful, move Gameobject back to its initial position.
                initialMove = Vector2.zero;
                ChoosenPiece = null;
                Highlight(); // need not change the highlighter position if move is not sucessful.
                // return;
            }
        }
    }

    private void SwitchTurn() // parameters to set at the end of valid move.
    {
        ChoosenPiece = null;  // resetting selection.
        initialMove = Vector2.zero; // resetting position.

        isRedTurn = !isRedTurn; // swapping turns at the end.
        isRed = !isRed;
        CheckWinCondition(); // checking if there is a win at the end of turn.

        CheckForAvailableMove(); // at each end of the turn there is check for movable tiger piece.
    }

    private void CheckWinCondition() // win condition 
    {
        var ps = FindObjectsOfType<count>(); // counting Goat gameobject on the board
        if (cash == 70) // by default score from quiz is set to 70 which is empty on board
        {
            count = 5; // for default goat count life for goat is set to 5 which is the maximum life.
        }
        else // in case score is diferent than 70
        {
            count = 5 - (20 - ps.Length); // life of goat according to its count.
        }
        
        if (count == 0) // win condition; if life of goat is 0 tiger wins
        {
            EndGame(); // calling end game function if count is 0
            return;
        }
        else // if it is not a win condition for either then display swapping of turn on canvas.
        {
            if (isRed)
                Alert("TIGER'S TURN");
            else
                Alert("GOAT'S TURN");
        }
    }

    private List<Piece> CheckForAvailableMove() // creating a list of Pieces
    {
        freezedPieces = new List<Piece>(); // new list since we are scanning new list everytime.

        // Check all the pieces
        if (isRedTurn) // since only tiger gameobject movement can be freezed.
        {
            for (int i = 3; i < 12; i += 2) // checking at each position every frame inside of the board.
            {
                for (int j = 0; j < 9; j += 2)
                {
                    if (pieces[i, j] != null && pieces[i, j].isRed == isRedTurn)  // if there is a piece at position and it is tiger turn
                        if (pieces[i, j].IsFreezedMove(pieces, i, j)) // if piece at position has a possible move
                            freezedPieces.Add(pieces[i, j]); // add it to the list of freezedPieces
                }
            }
        }
        Highlight(); // if there is a movable tiger piece then highlight it.
        return freezedPieces;
    }

    public void PieceMoveUpdate(Piece p) //Move selected piece along with cursor 
    {
        if (isRedTurn) // If it is a tiger turn
        {
            if (!Camera.main) // checking if there is camera in the game scene
            {
                Debug.Log("No camera found");
                return;
            }

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("BoardTiger"))) // raycast that we get from camera itself with a distance no more than 25f between object and camera.
            {
                p.transform.position = hit.point + new Vector3(0, 0.4f, 0); // moving piece upward by position 0.4f in y axis to avoid collision with other piece.

            }
        }

        else // if is a goat turn
        {
            if (!Camera.main)
            {
                Debug.Log("No camera found");
                return;
            }

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board"))) // different Layermask since there are different board for tiger and goat turn respectively.
            {
                p.transform.position = hit.point + new Vector3(0, 0.4f, 0); // moving position by 0.4f in y position.

            }
        }
    }

    private void Highlight() // highlighting movable tigers.
    {
        foreach (Transform t in Highlighter.transform)
        {
            t.position = Vector3.down * 100;  // at the end of turn moving highlighter back where it was.
        }

        if (freezedPieces.Count > 0) // if there are at least one movable tigers then active one of the highlighter.
            Highlighter.transform.GetChild(0).transform.position = freezedPieces[0].transform.position + Vector3.down * 0.0f; // moving highlighter up to a point visible for a tiger that is movable.

        if (freezedPieces.Count > 1) // if there are more than one then active highlighter index 0 and 1.
            Highlighter.transform.GetChild(1).transform.position = freezedPieces[1].transform.position + Vector3.down * 0.0f;

        if (freezedPieces.Count > 2) // if there are more than two then active highlighter index 0, 1 and 2.
            Highlighter.transform.GetChild(2).transform.position = freezedPieces[2].transform.position + Vector3.down * 0.0f;

        if (freezedPieces.Count > 3) // if there are more than three then active highlighter index 0, 1, 2,and 3.
            Highlighter.transform.GetChild(3).transform.position = freezedPieces[3].transform.position + Vector3.down * 0.0f;
    }

    private void ChoosePiece(int x, int y) // function to select piece for movement.
    {
        // Out of bounds
        if (isRedTurn) // checking if it is Tiger turn since bound for it is different from Goat 
        {
            if (x < 3 || x > 11 || y < 0 || y >= 9) // if x and y value are outside of the defined range Tiger selection does not happen.
                return;
        }
        else if (!isRedTurn) // If it is Goat turn and checking the bound 
        {
            if (x < 1 || x >= 14 || y < 0 || y >= 9) // if x and y value are outside of the defined range Goat selection does not happen.
                return;
        }              

        // if (pieces[x, y].isRed != isRedTurn)
        // return;

        Piece p = pieces[x, y]; // if mouse click is within the bound then checking if there is a Gameobject under the mouse click event
        if (p != null && p.isRed == isRed) // if there is a Gameobject under the click event and also Gameobject and turns are on the same side.
        {
            ChoosenPiece = p; // if there is piece under then select that piece.
            initialMove = MousePosition; // and Initial drag will start from selected point. 
            if (isRedTurn) 
            {
                audioSource1.PlayOneShot(TigerRoar); // playing selection Sound if it is tiger turn
            }
            else
            {
                audioSource.PlayOneShot(Goatmule); // If it is goat turn then play its sound.
            }
        }
    }

    private void GenerateNull() // Generate Tiger and Goat pieces to the Board
    {
        // Null function for not generating pieces on Board at the start of the Game.

    }

    private void GenerateTiger() // Generate Tiger Piece based on the score from Learning module of the game.
    {
        money = PlayerPrefs.GetInt("GoathighestScore"); // assined Value of Score from Learning module to variable money for switch Operation. 
        switch (money)                                 // PlayerPrefs.GetInt accesses player preferences between game sessions stored by the key of GoathighestScore
        {
            case 50:
                Tiger50(); // If value on Stored PlayerPref is 50 then calls function.
                break;

            case 60:
                Tiger60(); //If value on Stored PlayerPref is 60 then calls function.
                break;

            case 70:
                GenerateNull(); // This is the case for no piece generation at the start of the game. 
                break;

            default:
                Tigerdefault(); // Default case to generate all Pieces if above scores are not made in Learning Module.
                break;
        }
    }

    private void GenerateGoat() // assined Value of Score from Learning module to variable money for switch Operation.
    {
        cash = PlayerPrefs.GetInt("TigerhighestScore"); 
        switch (cash)
        {
            case 40:
                goat40();  // If value on Stored PlayerPref is 40 then calls function.
                break;

            case 50:
                goat50();  // If value on Stored PlayerPref is 50 then calls function.
                break;

            case 60:
                goat60(); // If value on Stored PlayerPref is 60 then calls function.
                break;

            case 70:
                GenerateNull(); // This is the case for no piece generation at the start of the game.
                break;

            default:
                goatdefault();  // Default case to generate all Pieces if above scores are not made in Learning Module.
                break;
        }
    }

    private void UpdateMousePosition() // function to position camera hit point
    {
        if (isRedTurn) // If its Tiger turn
        {
            boardGoat.SetActive(false);
            boardTiger.SetActive(true);
            if (!Camera.main) // checking for camera since raycast appears from camera and there is a camera in scene.
            {
                Debug.Log("No Camera Found");
                return;
            }

            RaycastHit hit; // from camera center point of view for input mouse position
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("BoardTiger"))) // raycast that we get from camera itself with a distance no more than 25f between object and camera. 
            {                                                                                                                        // value of raycast then stored inside of field hit.
                MousePosition.x = Mathf.RoundToInt(hit.point.x - Offsetforboard.x); // if raycast hit at board then we return the Integer x co-ordinate value of it.
                MousePosition.y = Mathf.RoundToInt(hit.point.z - Offsetforboard.z); // if raycast hit at board then we return the Integer z co-ordinate value of it
            }
            else
            {
                MousePosition.x = -1; // If raycast doesnot hit any surface then reset its value
                MousePosition.y = -1;
            }
        }
        
        else // If its Goat turn
        {
            boardGoat.SetActive(true);
            boardTiger.SetActive(false);
            if (!Camera.main)
            {
                Debug.Log("No Camera Found");
                return;
            }

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("Board")))
            {
                MousePosition.x = Mathf.RoundToInt(hit.point.x - Offsetforboard.x);
                MousePosition.y = Mathf.RoundToInt(hit.point.z - Offsetforboard.z);
            }
            else
            {
                MousePosition.x = -1;
                MousePosition.y = -1;
            }
        }
    }

    private void SpawnPiece(int x, int y) // Spawnning Pieces on Board with Parameter being the Value of GameObject position.
    {
        bool isTigerPiece = (x < 3 || x > 11) ? false : true; // ternary operator checking for tiger and goat piece generation based on its position value.
        GameObject go = Instantiate((isTigerPiece) ? TigerPiecePrefab : GoatPiecePrefab) as GameObject; // Instantiating with the referenced GameObject to a variable go.
        if (isTigerPiece && y == 8) // if is a TigerPiece and y position of it is 8 on the board then; set the rotation
        {
            go.transform.rotation = orientation; // Set the rotation of the transform to the Quaternion as initialized in orientation.
        }

        if (!isTigerPiece && (x < 3)) // if is a GoatPiece and x position of it is anything less than 3 on the board then; set the rotation.
        {
            go.transform.rotation = orientationGoat; // Set the rotation of the transform to the Quaternion as initialized in orientationGoat.
        }

        if (!isTigerPiece && (x > 11 && x < 14)) // if is a GoatPiece and x position of it is 12 or 13 on the board then; set the rotation.
        {
            go.transform.rotation = orientationGoat1; // Set the rotation of the transform to the Quaternion as initialized in orientationGoat.
        }
        go.transform.SetParent(transform); // Set the parent of the transform to its Local orientation.
        Piece p = go.GetComponent<Piece>(); // put the component piece on top of the actual piece
        pieces[x, y] = p; // placing component piece inside of the Piece array .
        MoveGameObject(p, x, y); // once placed then Piece is moved to its position.
    }

    private void MoveGameObject(Piece p, int x, int y) // controlled for the movement of the Pieces.
    {        
        p.transform.position = (Vector3.right * x) + (Vector3.forward * y) + Offsetforboard + OffsetforPiece;  // set the transform(movement) of the piece along x and y axis of the world + offsets to move
    }                                                                                                    // the origin of the world to value 5 in x axis and 2 in the y axis i.e. making (-5, -2) as the origin of the board.
                                                       
    private void EndGame() // ending game after win condition
    {
        if (isRedTurn)
        {
            Alert("GOAT HAS WON !!"); // if tiger wins then this canvas is displayed.
        }

        else
        {
            Alert(" TIGER HAS WON !!"); // if goat wins then this canvas is displayed.
        }
        gameIsOver = true; // setting bollean to trigger gameover UI.
        gameOverUI.SetActive(true);  // setting gameover UI active after game is over.
    }

    public void Alert(string text) // for swapping text on game screen.
    {
        ActiveCanvasCaution.GetComponentInChildren<Text>().text = text; // popping swapping turn text and since text for this is child of the main canvas. 
        ActiveCanvasCaution.alpha = 1; // for controlling the opacity of canvas. 
        ActiveCanvas = true; 
    }

    public void ReviseCanvasAlert() // Swapping canvas opacity  
    {
        if (ActiveCanvas) // if triggered 
        {
                ActiveCanvasCaution.alpha = 1; // letting opacity to its maximim.
        }
    }

    void SetCountText() // displaying lives count on screen canvas.
    {
        countText.text = "  LIVES: "; 
    }

    private void Tigerdefault() // for default case 
    {
        for (int y = 0; y < 9; y += 8) // generating Tiger Piece at each corner of the the Board based on position
        {                               
            for (int x = 3; x < 12; x += 8) // (0,3), (0,11), (8,3), (8,11), the four positions of the Tiger 
            {
                // Generate our Piece
                SpawnPiece(x, y); // for Spawnning Pieces on the Board.
            }
        }
    }

    private void Tiger50() // If the points Scored is 50 on Learning mmodule 
    {
        for (int y = 8; y < 9; y += 8) // Spawnning Tigers out at Position (8,3), (8,11)
        {
            for (int x = 3; x < 12; x += 8)
            {
                // Generate our Piece
                SpawnPiece(x, y);
            }
        }

        for (int y = 0; y < 1; y++) //Spawnning Tiger out at Position (0,11) excluding one at (0,3)
        {
            for (int x = 11; x < 12; x++)
            {
                // Generate our Piece
                SpawnPiece(x, y);
            }
        }
    }

    private void Tiger60() // If the points Scored is 50 on Learning mmodule
    {
        for (int y = 0; y < 1; y++) //Spawnning Tiger out at Position (0,11)and (0,3) excluding at (8,3), (8,11)
        {
            for (int x = 3; x < 12; x += 8)
            {
                // Generate our Piece
                SpawnPiece(x, y);
            }
        }
    }

    private void goatdefault() // for default case of Goat Piece Spawnning
    {
        // Generate Goat team left of board
        for (int x = 1; x < 3; x++)
        {
            for (int y = 2; y < 7; y++)
            {
                // Generate our Piece
                SpawnPiece(x, y);
            }
        }

        // Generate Goat team Right of board
        for (int x = 12; x < 14; x++)
        {
            for (int y = 2; y < 7; y++)
            {
                // Generate our Piece
                SpawnPiece(x, y);
            }
        }
    }

    private void goat40() // If the points Scored is 40 on Learning mmodule
    {
        // Generate Goat team left of board
        for (int x = 1; x < 2; x++)
        {
            for (int y = 3; y < 7; y++)
            {
                // Generate our Piece
                SpawnPiece(x, y);
            }
        }

        for (int x = 2; x < 3; x++)
        {
            for (int y = 2; y < 7; y++)
            {
                // Generate our Piece
                SpawnPiece(x, y);
            }
        }

        // Generate Goat team Right of board
        for (int x = 12; x < 14; x++)
        {
            for (int y = 2; y < 7; y++)
            {
                // Generate our Piece
                SpawnPiece(x, y);
            }
        }
    }

    private void goat50() // If the points Scored is 50 on Learning mmodule
    {
        // Generate Goat team left of board
        for (int x = 1; x < 3; x++)
        {
            for (int y = 3; y < 7; y++)
            {
                // Generate our Piece
                SpawnPiece(x, y);
            }
        }

        // Generate Goat team Right of board
        for (int x = 12; x < 14; x++)
        {
            for (int y = 2; y < 7; y++)
            {
                // Generate our Piece
                SpawnPiece(x, y);
            }
        }
    }

    private void goat60() // If the points Scored is 60 on Learning mmodule
    {
        // Generate Goat team left of board
        for (int x = 1; x < 3; x++)
        {
            for (int y = 3; y < 7; y++)
            {
                // Generate our Piece
                SpawnPiece(x, y);
            }
        }

        // Generate Goat team Right of board
        for (int x = 12; x < 14; x++)
        {
            for (int y = 3; y < 7; y++)
            {
                // Generate our Piece
                SpawnPiece(x, y);
            }
        }

        for (int x = 12; x < 13; x++)
        {
            for (int y = 2; y < 3; y++)
            {
                // Generate our Piece
                SpawnPiece(x, y);
            }
        }
    }

    private void life() // life count of goat
    {
        health = count; // initialzing count value from  CheckWinCondition function
        switch (health)  
        {
            case 5:
                lifecube1.gameObject.SetActive(true); // if health count is 5 then all 5 health gameobjects are set active.
                lifecube2.gameObject.SetActive(true);
                lifecube3.gameObject.SetActive(true);
                lifecube4.gameObject.SetActive(true);
                lifecube5.gameObject.SetActive(true);
                break;

            case 4:
                lifecube1.gameObject.SetActive(true); //if health count is 4 then 4 of the gameobjects are set active.
                lifecube2.gameObject.SetActive(true);
                lifecube3.gameObject.SetActive(true);
                lifecube4.gameObject.SetActive(true);
                lifecube5.gameObject.SetActive(false);
                break;

            case 3:
                lifecube1.gameObject.SetActive(true); //if health count is 3 then 3 of the gameobjects are set active.
                lifecube2.gameObject.SetActive(true);
                lifecube3.gameObject.SetActive(true);
                lifecube4.gameObject.SetActive(false);
                lifecube5.gameObject.SetActive(false);
                break;

            case 2:
                lifecube1.gameObject.SetActive(true); //if health count is 2 then 2 of the gameobjects are set active.
                lifecube2.gameObject.SetActive(true);
                lifecube3.gameObject.SetActive(false);
                lifecube4.gameObject.SetActive(false);
                lifecube5.gameObject.SetActive(false);
                break;

            case 1:
                lifecube1.gameObject.SetActive(true); //if health count is 1 then 1 of the gameobject is set active.
                lifecube2.gameObject.SetActive(false);
                lifecube3.gameObject.SetActive(false);
                lifecube4.gameObject.SetActive(false);
                lifecube5.gameObject.SetActive(false);
                break;

            case 0:
                lifecube1.gameObject.SetActive(false); // none of the gameobject is active and this is the win scenario for tiger.
                lifecube2.gameObject.SetActive(false);
                lifecube3.gameObject.SetActive(false);
                lifecube4.gameObject.SetActive(false);
                lifecube5.gameObject.SetActive(false);
                break;

                //default:

        }
    }

    private void TigerBlocked() // setting UI count display for tiger
    {
        freezedPieces = CheckForAvailableMove(); // initializing tiger freezed count each time there is an update.
        if (isRedTurn)
        {
            var ss = FindObjectsOfType<tigercount>(); // counting the number of Tiger Gameobject on board.
            tigerblocked = ss.Length - freezedPieces.Count;  // counting blocked tigers.
            tigerfree = ss.Length - tigerblocked;  // counting movable tigers.
            tigerblockedText.text = " FREEZED:      " + tigerblocked.ToString(); // displaying unmovable tiger count GameObject on canvas.
            tigerfreeText.text = " FREED:         " + tigerfree.ToString(); // displaying movable tiger count GameObject on canvas.
        }
        

    }
}
