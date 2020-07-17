using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBowl_Library
{
    public enum Instructions
    {
        Exit_Software,

        LogIn,
        SignIn,
        LogOut,

        Coach_GetAll,
        Coach_GetById,
        Coach_GetByName,

        Team_GetAllFromCoach,
        Team_GetById,
        Team_New,


        /*
        Topic_GetAll,
        Topic_GetByIc,
        Topic_New,

        Chat_GetAll,
        Chat_GetById,
        Chat_New,

        Member_Join,
        Member_Leave,

        Message_New,
        Message_GetById
        */
    }
}
