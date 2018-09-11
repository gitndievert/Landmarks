using UnityEngine;
using System.Collections;
using AlphaPuzzle.State;

public static partial class Extensions
{
    public static IEnumerator Lerp(this Transform trans, Vector3 targetPosition, float speedInSec)
    {
        float time = 0f;
        //Disable Drags
        while (true)
        {
            GameState.DragEnabled = false;
            time += Time.deltaTime * speedInSec;
            trans.position = Vector3.Lerp(trans.position, targetPosition, time);

            //Stop on Final Position
            if (trans.position == targetPosition)
            {
                GameState.DragEnabled = true;                
                yield break;
            }

            yield return null;
        }
    }      
    
    public static IEnumerator ScaleDown(this Transform trans, float size, float speedInSec)
    {
        float time = 0f;

        while (true)
        {
            
            time += Time.deltaTime;
            var scale = trans.localScale;
            trans.localScale -= new Vector3(scale.x, scale.y, scale.z) * Time.deltaTime * size;

            if (trans.localScale.x <= size)
                yield break;

            yield return null;
            
        }
    }

    public static IEnumerator ScaleUp(this Transform trans, float size, float speedInSec)
    {
        float time = 0f;

        while (true)
        {

            time += Time.deltaTime;
            var scale = trans.localScale;
            trans.localScale += new Vector3(scale.x, scale.y, scale.z) * Time.deltaTime * size;

            if (trans.localScale.x >= size)
                yield break;

            yield return null;

        }
    }    


}
