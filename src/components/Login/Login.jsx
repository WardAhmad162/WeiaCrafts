import React, { useState } from 'react';
import { Container, Row, Col, Form, Button, InputGroup } from 'react-bootstrap';
// Assuming you might want icons for the password toggle
import { EyeFill, EyeSlashFill } from 'react-bootstrap-icons'; // Example, install if needed: npm install react-bootstrap-icons
import './Login.css'; // Import the CSS file

// Important: Ensure you have 'react-bootstrap' and 'bootstrap' installed
// npm install react-bootstrap bootstrap
// You also need to import Bootstrap CSS in your main entry file (e.g., index.js or App.js)
// import 'bootstrap/dist/css/bootstrap.min.css';

function LoginForm() {
  // State for form fields, similar to Signup.jsx
  const [formData, setFormData] = useState({
    email: '',
    password: '',
    rememberMe: false,
  });

  // State for password visibility
  const [showPassword, setShowPassword] = useState(false);

  // Handle input changes, similar to Signup.jsx
  const handleChange = (e) => {
    const { id, value, type, checked } = e.target;
    setFormData(prevData => ({
      ...prevData,
      [id]: type === 'checkbox' ? checked : value,
    }));
    // Add validation logic here if needed, similar to Signup.jsx
  };

  // Toggle password visibility
  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  // Handle form submission (add your login logic here)
  const handleSubmit = (e) => {
    e.preventDefault();
    console.log('Login Data:', formData);
    // Add actual login API call or logic here
  };

  return (
    <Container fluid className="login-container d-flex align-items-center justify-content-center">
      <div className="login-card shadow-sm p-4">
        <Row className="g-0">
          {/* Left Column: Form */}
          <Col md={6} className="login-form-col p-md-5 p-4 d-flex flex-column justify-content-center">
            <h1 className="login-title text-gradient fw-bold mb-3">Welcome back!</h1>
            <p className="login-subtitle text-muted-olive-green mb-4">
              Today is a new creative day. Sign in now to shape the future with us.
            </p>

            {/* Use onSubmit on the Form component */}
            <Form onSubmit={handleSubmit}>
              {/* Email Input - Using id="email" to match state key */}
              <Form.Group className="mb-3" controlId="email">
                <Form.Label className="login-label text-muted-olive-green">Email</Form.Label>
                <Form.Control
                  type="email"
                  placeholder="Enter email"
                  className="login-input bg-beige text-dark-brownish-gray"
                  value={formData.email} // Bind value to state
                  onChange={handleChange} // Use unified handler
                  required // Add basic validation if needed
                />
              </Form.Group>

              {/* Password Input - Using id="password" to match state key */}
              <Form.Group className="mb-4" controlId="password">
                <Form.Label className="login-label text-muted-olive-green">Password</Form.Label>
                <InputGroup>
                  <Form.Control
                    type={showPassword ? 'text' : 'password'} // Dynamic type based on state
                    placeholder="Password"
                    className="login-input login-input-password bg-beige text-dark-brownish-gray"
                    value={formData.password} // Bind value to state
                    onChange={handleChange} // Use unified handler
                    required // Add basic validation if needed
                  />
                  {/* Password Toggle Button */}
                  <Button
                    variant="outline-secondary"
                    className="login-password-toggle"
                    onClick={togglePasswordVisibility} // Add onClick handler
                  >
                    {/* Conditionally render eye icon */}
                    {showPassword ? <EyeSlashFill size={16} /> : <EyeFill size={16} />}
                  </Button>
                </InputGroup>
              </Form.Group>

              {/* Remember Me & Forgot Password */}
              <div className="d-flex justify-content-between align-items-center mb-4">
                {/* Remember Me Checkbox - Using id="rememberMe" */}
                <Form.Check
                  type="checkbox"
                  id="rememberMe"
                  label="Remember me"
                  className="login-label text-muted-olive-green"
                  checked={formData.rememberMe} // Bind checked to state
                  onChange={handleChange} // Use unified handler
                />
                <a href="#!" className="login-link text-peachy-highlight text-decoration-none"
                style={{ color: "var(--peachy-highlight)" }}>Forgot Password?</a>
              </div>

              {/* Sign In Button */}
              <Button type="submit" className="btn-peachy w-100 mb-3">
                Sign in
              </Button>

              {/* Or */}
              <div className="d-flex align-items-center my-3">
                  <div className="flex-grow-1 border-top border-peachy-highlight"></div>
                  <span className="mx-3 text-peachy-highlight">Or</span>
                  <div className="flex-grow-1 border-top border-peachy-highlight"></div>
                </div>

              {/* Sign up with Google Button */}
              <button
                  type="button"
                  className="btn btn-peachy w-100 mb-3"
                >
                  <img src="img/google.png" alt="Google" width="24" height="24" />
                  <span>Sign up with Google</span>
                </button>

              {/* Link to Sign Up */}
              <div className="text-center">
                <p className="login-signup-text text-dark-brownish-gray">
                  Don't you have an account? <a href="#!" className="fw-semibold text-decoration-none"
                  style={{ color: "var(--peachy-highlight)" }}>Sign up</a>
                </p>
              </div>
            </Form>
          </Col>

          {/* Right Column: Image */}
          <Col md={6} className="login-image-col d-none d-md-block p-0">
            <img
              src="/img/login-background.png" // Make sure this path is correct in your project
              alt="Woman baking in a kitchen"
              className="login-image img-fluid"
            />
          </Col>
        </Row>
      </div>
    </Container>
   );
}

export default LoginForm;
