using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




public enum State
{
    Students,
    Teachers,
    Courses
   
}



namespace admin_ui.table
{
    internal class TableStateMachine
    {
        public State CurrentState { get; private set; }


        // Method to authenticate a user
        public void setState(string newState)
        {
            newState = newState.ToLower();

        }
        
 

    }

}



