import { Link } from "react-router";
import Button from "../../components/Button";

function Register() {
  return (
    <div className="form-container">
      <div className="form__content">
        <form action="POST" className="form">
          <h4>Registration</h4>
          <label htmlFor="email">Email</label>
          <input className="form__input" type="email" name="email" required />
          <label htmlFor="password">Password</label>
          <input
            className="form__input"
            type="password"
            name="password"
            required
          />
          <label htmlFor="confirm-password">Confirm Password</label>
          <input
            className="form__input"
            type="password"
            name="confirm-password"
            required
          />
          <Link to={"/login"}>
            <p>Already have an account?</p>
          </Link>
          <Button>Register</Button>
        </form>
      </div>
    </div>
  );
}

export default Register;
