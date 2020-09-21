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
        Coach_GetInvitations,

        Team_GetAllFromCoach,
        Team_GetById,
        Team_New,
        Team_AddPlayer,
        Team_RemovePlayer,

        Player_SearchByName,
        Player_LevelUp,

        League_New,
        League_GetAllForCoach,
        League_GetMembersData,
        League_InviteCoach,
        League_InviteTeam
    }
}
