export class UserLogInInfo{

    constructor(
        private accessToken: string,
        private refreshToken: string,
        private expires_in: Date
      ) {}

      get token() {
        if (!this.expires_in || new Date() > this.expires_in) {
          return null;
        }
        return this.accessToken;
      }
}
