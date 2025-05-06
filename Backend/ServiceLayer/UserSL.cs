using IntroSE.Kanban.Backend.BussinesLayer.User;

public class UserSL
{
    private readonly string email;

    internal UserSL(UserBL ubl)
    {
        this.email = ubl.Email;
    }

    public string Email
    {
        get { return email; }
    }
}
