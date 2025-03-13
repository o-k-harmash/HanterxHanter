import Button from "../../components/Button";
import { Link } from "react-router";

function Login() {
  return (
    <div className="form-container">
      <div className="form__content">
        <form action="POST" className="form">
          <h4>Logining</h4>
          <label htmlFor="email">Email</label>
          <input className="form__input" type="email" name="email" required />
          <label htmlFor="password">Password</label>
          <input
            className="form__input"
            type="password"
            name="password"
            required
          />
          <Link to={"/register"}>
            <p>Do not have an account?</p>
          </Link>
          <Button>Login</Button>
        </form>
      </div>
    </div>
  );
}

export default Login;
