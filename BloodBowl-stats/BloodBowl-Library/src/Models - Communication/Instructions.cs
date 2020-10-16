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
        Coach_SearchByName,
        Coach_SearchByNameExceptSelf,
        Coach_GetInvitations,

        Team_GetAllFromCoach,
        Team_GetById,
        Team_New,
        Team_Delete,
        Team_AddPlayer,
        Team_RemovePlayer,

        Player_LevelUp,

        League_New,
        League_GetAllForCoach,
        League_GetMembersData,

        League_InviteCoachCreate,
        League_InviteCoachAccept,
        League_InviteCoachRefuse,
        League_RemoveCoach,
        League_Coach_Leave,
        League_Coach_LeaveAsCEO,

        League_InviteTeamCreate,
        League_InviteTeamAccept,
        League_InviteTeamRefuse,
        League_RemoveTeam
    }
}

// 12 - UE : une identité culturelle imaginaire ?
// 6ème séance