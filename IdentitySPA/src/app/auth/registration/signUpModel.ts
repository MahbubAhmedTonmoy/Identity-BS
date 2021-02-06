export class SignUp{
    public FullName : string;
    public UserName : string;
    public Email : string;
    public Password : string;
    public ConfirmPassword: string;
    public Role: [];
    public TwoFectorAuth : boolean;
    constructor(FullName: string, UserName: string, Email: string, Password: string, ConfirmPassword:string,
        Role: [],TwoFectorAuth:boolean ) {
        this.FullName = FullName;
        this.UserName = UserName;
        this.Email = Email;
        this.Password = Password;
        this.ConfirmPassword = ConfirmPassword;
        this.Role = Role;
        this.TwoFectorAuth = TwoFectorAuth;
    }
}