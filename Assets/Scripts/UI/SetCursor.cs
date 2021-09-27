using UnityEngine;

namespace UI
{
    public class SetCursor: MonoBehaviour
    {
        // You must set the cursor in the inspector.
        public Texture2D cursor; 

        void Start(){
    
            //set the cursor origin to its centre. (default is upper left corner)
            var cursorOffset = new Vector2(0, 0);
     
            //Sets the cursor to the Crosshair sprite with given offset 
            //and automatic switching to hardware default if necessary
            Cursor.SetCursor(cursor, cursorOffset, CursorMode.Auto);
        }
    }
}