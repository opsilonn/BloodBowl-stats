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
        Team_AddPlayer,
        Team_RemovePlayer,

        Player_LevelUp,

        League_New,
        League_GetAllForCoach,
        League_GetMembersData
    }
}
