using SBK.Unity;
using UnityEngine;

public abstract class BoardManager<T> : PSingle<T> where T : PSingle<T>
{
    public GameObject PieceCollection;   

    public abstract bool CheckForWin();
        
    protected GameObject PiecePrefab;    
}
