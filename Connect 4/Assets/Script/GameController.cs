using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ConnectFour
{
	public class GameController : MonoBehaviour
	{
		enum Piece
		{
			Empty = 0,
			Blue = 1,
			Red = 2
		}

		public int numRows = 6;
		public int numColumns = 7;
		public int numPiecesToWin = 4;
		private bool allowDiagonally = true;
		public float dropTime = 4f;

		public GameObject pieceRed;
		public GameObject pieceYellow;
		public GameObject pieceField;

		public GameObject winningText;
		public string playerWonText = "You Won!";
		public string playerLoseText = "You Lose!";
		public string drawText = "Draw!";
		GameObject gameObjectField;
		GameObject gameObjectTurn;

		
		int[,] field;

		bool isPlayersTurn = true;
		bool isLoading = true;
		bool isDropping = false;
		bool mouseButtonPressed = false;

		bool gameOver = false;
		bool isCheckingForWinner = false;

		// Use this for initialization
		void Start()
		{
			int max = Mathf.Max(numRows, numColumns);

			if (numPiecesToWin > max)
				numPiecesToWin = max;

			CreateField();

			isPlayersTurn = System.Convert.ToBoolean(Random.Range(0, 1));

			
		}

		void CreateField()
		{
			winningText.SetActive(false);

			isLoading = true;

			gameObjectField = GameObject.Find("Field");
			if (gameObjectField != null)
			{
				DestroyImmediate(gameObjectField);
			}
			gameObjectField = new GameObject("Field");

			field = new int[numColumns, numRows];
			for (int x = 0; x < numColumns; x++)
			{
				for (int y = 0; y < numRows; y++)
				{
					field[x, y] = (int)Piece.Empty;
					GameObject g = Instantiate(pieceField, new Vector3(x, y * -1, -1), Quaternion.identity) as GameObject;
					g.transform.parent = gameObjectField.transform;
				}
			}

			isLoading = false;
			gameOver = false;

			// center camera
			Camera.main.transform.position = new Vector3(
				(numColumns - 1) / 2.0f, -((numRows - 1) / 2.0f), Camera.main.transform.position.z);

			winningText.transform.position = new Vector3(
				(numColumns - 1) / 2.0f, -((numRows - 1) / 2.0f) + 1, winningText.transform.position.z);

			
		}

		GameObject SpawnPiece()
		{
			Vector3 spawnPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			if (!isPlayersTurn)
			{
				List<int> moves = GetPossibleMoves();

				if (moves.Count > 0)
				{
					int column = moves[Random.Range(0, moves.Count)];

					spawnPos = new Vector3(column, 0, 0);
				}
			}

			GameObject g = Instantiate(
					isPlayersTurn ? pieceYellow : pieceRed, // is players turn = spawn blue, else spawn red
					new Vector3(
					Mathf.Clamp(spawnPos.x, 0, numColumns - 1),
					gameObjectField.transform.position.y + 1, 0), // spawn it above the first row
					Quaternion.identity) as GameObject;

			return g;
		}

		void Update()
		{
			if (isLoading)
				return;

			if (isCheckingForWinner)
				return;

			if (gameOver)
			{
				//SoundManager.SM.GameoverSoundClip();
				winningText.SetActive(true);
				return;
				
			}

			if (isPlayersTurn)
			{
				if (gameObjectTurn == null)
				{
					gameObjectTurn = SpawnPiece();
				}
				else
				{
					// update the objects position
					Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					gameObjectTurn.transform.position = new Vector3(
						Mathf.Clamp(pos.x, 0, numColumns - 1),
						gameObjectField.transform.position.y + 1, 0);

					// click the left mouse button to drop the piece into the selected column
					if (Input.GetMouseButtonDown(0) && !mouseButtonPressed && !isDropping)
					{
						mouseButtonPressed = true;
						SoundManager.SM.SoundClipPlay();

						StartCoroutine(dropPiece(gameObjectTurn));
					}
					else
					{
						mouseButtonPressed = false;
					}
				}
			}
			else
			{
				if (gameObjectTurn == null)
				{
					gameObjectTurn = SpawnPiece();
				}
				else
				{
					if (!isDropping)
						StartCoroutine(dropPiece(gameObjectTurn));
				}
			}
		}

		public List<int> GetPossibleMoves()
		{
			List<int> possibleMoves = new List<int>();
			for (int x = 0; x < numColumns; x++)
			{
				for (int y = numRows - 1; y >= 0; y--)
				{
					if (field[x, y] == (int)Piece.Empty)
					{
						possibleMoves.Add(x);
						break;
					}
				}
			}
			return possibleMoves;
		}
		IEnumerator dropPiece(GameObject gObject)
		{
			isDropping = true;

			Vector3 startPosition = gObject.transform.position;
			Vector3 endPosition = new Vector3();

			int x = Mathf.RoundToInt(startPosition.x);
			startPosition = new Vector3(x, startPosition.y, startPosition.z);

			bool foundFreeCell = false;
			for (int i = numRows - 1; i >= 0; i--)
			{
				if (field[x, i] == 0)
				{
					foundFreeCell = true;
					field[x, i] = isPlayersTurn ? (int)Piece.Blue : (int)Piece.Red;
					endPosition = new Vector3(x, i * -1, startPosition.z);

					break;
				}
			}

			if (foundFreeCell)
			{
				
				GameObject g = Instantiate(gObject) as GameObject;
				gameObjectTurn.GetComponent<Renderer>().enabled = false;

				float distance = Vector3.Distance(startPosition, endPosition);

				float t = 0;
				while (t < 1)
				{
					t += Time.deltaTime * dropTime * ((numRows - distance) + 1);

					g.transform.position = Vector3.Lerp(startPosition, endPosition, t);
					yield return null;
				}

				g.transform.parent = gameObjectField.transform;
				DestroyImmediate(gameObjectTurn);
				StartCoroutine(Won());
				while (isCheckingForWinner)
					yield return null;

				isPlayersTurn = !isPlayersTurn;
			}

			isDropping = false;

			yield return 0;
		}
		IEnumerator Won()
		{
			isCheckingForWinner = true;

			for (int x = 0; x < numColumns; x++)
			{
				for (int y = 0; y < numRows; y++)
				{
					int layermask = isPlayersTurn ? (1 << 8) : (1 << 9);

					if (field[x, y] != (isPlayersTurn ? (int)Piece.Blue : (int)Piece.Red))
					{
						continue;
					}

					RaycastHit[] hitsHorz = Physics.RaycastAll(
						new Vector3(x, y * -1, 0),
						Vector3.right,
						numPiecesToWin - 1,
						layermask);

					if (hitsHorz.Length == numPiecesToWin - 1)
					{
						gameOver = true;
						break;
					}
					RaycastHit[] hitsVert = Physics.RaycastAll(
						new Vector3(x, y * -1, 0),
						Vector3.up,
						numPiecesToWin - 1,
						layermask);

					if (hitsVert.Length == numPiecesToWin - 1)
					{
						gameOver = true;
						break;
					}

					
					if (allowDiagonally)
					{
						
						float length = Vector2.Distance(new Vector2(0, 0), new Vector2(numPiecesToWin - 1, numPiecesToWin - 1));

						RaycastHit[] hitsDiaLeft = Physics.RaycastAll(
							new Vector3(x, y * -1, 0),
							new Vector3(-1, 1),
							length,
							layermask);

						if (hitsDiaLeft.Length == numPiecesToWin - 1)
						{
							gameOver = true;
							break;
						}

						RaycastHit[] hitsDiaRight = Physics.RaycastAll(
							new Vector3(x, y * -1, 0),
							new Vector3(1, 1),
							length,
							layermask);

						if (hitsDiaRight.Length == numPiecesToWin - 1)
						{
							gameOver = true;
							break;
						}
					}

					yield return null;
				}

				yield return null;
			}

			if (gameOver == true)
			{
				winningText.GetComponent<TextMesh>().text = isPlayersTurn ? playerWonText : playerLoseText;
			}
			else
			{
				if (!FieldContainsEmptyCell())
				{
					gameOver = true;
					winningText.GetComponent<TextMesh>().text = drawText;
				}
			}

			isCheckingForWinner = false;

			yield return 0;
		}

		bool FieldContainsEmptyCell()
		{
			for (int x = 0; x < numColumns; x++)
			{
				for (int y = 0; y < numRows; y++)
				{
					if (field[x, y] == (int)Piece.Empty)
						return true;
				}
			}
			return false;
		}
	}
}
