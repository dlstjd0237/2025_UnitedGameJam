using UnityEngine;

[CreateAssetMenu(menuName = "BIS/SO/Stat/PieceSO")]
public class StatPieceSO : ScriptableObject
{
    [SerializeField] private StatPiece _piece; public StatPiece Piece { get { return _piece; } }
}
