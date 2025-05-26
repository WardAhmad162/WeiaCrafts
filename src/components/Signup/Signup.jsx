import React, { useState } from "react";
import "./Signup.css";
import '../../index.css';
const Signup = () => {
    const [formData, setFormData] = useState({
    username: "",
    email: "",
    gender: "",
    dob: "",
    password: "",
    confirmPassword: ""
  });
  // إدارة حالة أخطاء التحقق
  const [errors, setErrors] = useState({
    username: "",
    email: "",
    gender: "",
    dob: "",
    password: "",
    confirmPassword: ""
  });

  // إدارة حالة الإرسال
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [submitStatus, setSubmitStatus] = useState({
    success: false,
    message: ""
  });
  
  // تحديث حالة النموذج عند تغيير أي حقل
  const handleChange = (e) => {
    const { id, value, type, checked, name } = e.target;
    
    // التعامل مع حقول الراديو بشكل خاص
    if (type === "radio") {
      setFormData({
        ...formData,
        [name]: value
      });
    } else {
      setFormData({
        ...formData,
        [id]: value
      });
    }
    
    // التحقق من صحة الحقل عند تغييره
    validateField(id || name, value);
  };

  // التحقق من صحة حقل معين
  const validateField = (fieldName, value) => {
    let errorMessage = "";

    switch (fieldName) {
      case "username":
        if (!value) {
          errorMessage = "اسم المستخدم مطلوب";
        } else if (value.length < 3) {
          errorMessage = "يجب أن يكون اسم المستخدم 3 أحرف على الأقل";
        }
        break;
        
      case "email":
        if (!value) {
          errorMessage = "البريد الإلكتروني مطلوب";
        } else if (!/\S+@\S+\.\S+/.test(value)) {
          errorMessage = "يرجى إدخال بريد إلكتروني صحيح";
        }
        break;
        
      case "gender":
        if (!value) {
          errorMessage = "يرجى اختيار الجنس";
        }
        break;
        
      case "dob":
        if (!value) {
          errorMessage = "تاريخ الميلاد مطلوب";
        } else {
          const today = new Date();
          const birthDate = new Date(value);
          const age = today.getFullYear() - birthDate.getFullYear();
          
          if (age < 13) {
            errorMessage = "يجب أن يكون عمرك 13 عامًا على الأقل";
          }
        }
        break;
        
      case "password":
        if (!value) {
          errorMessage = "كلمة المرور مطلوبة";
        } else if (value.length < 8) {
          errorMessage = "يجب أن تكون كلمة المرور 8 أحرف على الأقل";
        } else if (!/(?=.*[a-z])(?=.*[A-Z])(?=.*\d)/.test(value)) {
          errorMessage = "يجب أن تحتوي كلمة المرور على حرف كبير وحرف صغير ورقم على الأقل";
        }
        
        // التحقق من تطابق كلمتي المرور إذا تم إدخال تأكيد كلمة المرور
        if (formData.confirmPassword && value !== formData.confirmPassword) {
          setErrors(prev => ({
            ...prev,
            confirmPassword: "كلمتا المرور غير متطابقتين"
          }));
        } else if (formData.confirmPassword) {
          setErrors(prev => ({
            ...prev,
            confirmPassword: ""
          }));
        }
        break;
        
      case "confirmPassword":
        if (!value) {
          errorMessage = "تأكيد كلمة المرور مطلوب";
        } else if (value !== formData.password) {
          errorMessage = "كلمتا المرور غير متطابقتين";
        }
        break;
        
      default:
        break;
    }

    // تحديث حالة الأخطاء
    setErrors(prev => ({
      ...prev,
      [fieldName]: errorMessage
    }));
    
    return !errorMessage; // إرجاع true إذا كان الحقل صحيحًا
  };

  // التحقق من صحة النموذج بالكامل
  const validateForm = () => {
    let isValid = true;
    
    // التحقق من كل حقل
    Object.keys(formData).forEach(fieldName => {
      const value = formData[fieldName];
      const fieldIsValid = validateField(fieldName, value);
      if (!fieldIsValid) {
        isValid = false;
      }
    });
    
    return isValid;
  };
  return (
    <div className="signUp bg-[#CBC3A74D]  shadow-md p-4 h-full rounded-3xl ">
      <div className="container">
        <div className="row">
          <div className="col-xl-6 py-5">
            <img
              src="img/sign-up-background.png"
              alt="Craftsman"
              className="rounded-3xl shadow-lg img-fluid"
            />
          </div>

          <div className="h-100 py-5 col-xl-6"  >
            <div className="w-100 p-4 rounded ">
              <h2 className="text-center fw-bold text-gradient mb-4">
                Set Up Your Account
              </h2>
              <form>
                {/* Username */}
                <div className="mb-3">
                  <label
                    htmlFor="username"
                    className="form-label text-muted-olive-green"
                  >
                    User name
                  </label>
                  <input
                    type="text"
                    className="form-control bg-beige text-dark-brownish-gray"
                    id="username"
                    placeholder="User name"
                  />
                </div>

                {/* Email */}
                <div className="mb-3">
                  <label
                    htmlFor="email"
                    className="form-label text-muted-olive-green"
                  >
                    Email
                  </label>
                  <input
                    type="email"
                    className="form-control bg-beige text-dark-brownish-gray"
                    id="email"
                    placeholder="Email"
                  />
                </div>

                {/* Gender */}
                <div className="mb-3">
                  <label className="form-label d-block text-muted-olive-green">
                    Gender
                  </label>
                  <div className="form-check form-check-inline">
                    <input
                      className="form-check-input"
                      type="radio"
                      name="gender"
                      id="female"
                      value="female"
                    />
                    <label className="form-check-label" htmlFor="female">
                      Female
                    </label>
                  </div>
                  <div className="form-check form-check-inline">
                    <input
                      className="form-check-input"
                      type="radio"
                      name="gender"
                      id="male"
                      value="male"
                    />
                    <label className="form-check-label" htmlFor="male">
                      Male
                    </label>
                  </div>
                </div>

                {/* Date of Birth */}
                <div className="mb-3">
                  <label
                    htmlFor="dob"
                    className="form-label text-muted-olive-green"
                  >
                    Date Of Birth
                  </label>
                  <input
                    type="date"
                    className="form-control bg-beige text-dark-brownish-gray"
                    id="dob"
                  />
                </div>

                {/* Password */}
                <div className="mb-3">
                  <label
                    htmlFor="password"
                    className="form-label text-muted-olive-green"
                  >
                    Password
                  </label>
                  <input
                    type="password"
                    className="form-control"
                    id="password"
                    placeholder="Password"
                  />
                </div>

                {/* Confirm Password */}
                <div className="mb-3">
                  <label
                    htmlFor="confirmPassword"
                    className="form-label text-muted-olive-green"
                  >
                    Confirm Password
                  </label>
                  <input
                    type="password"
                    className="form-control"
                    id="confirmPassword"
                    placeholder="Confirm Password"
                  />
                </div>

                {/* Create Account */}
                <button type="submit" className="btn btn-peachy w-100 mb-3">
                  Create Account
                </button>

                {/* Or */}
                <div className="d-flex align-items-center my-3">
                  <div className="flex-grow-1 border-top border-peachy-highlight"></div>
                  <span className="mx-3 text-peachy-highlight">Or</span>
                  <div className="flex-grow-1 border-top border-peachy-highlight"></div>
                </div>

                {/* Sign up with Google */}
                <button
                  type="button"
                  className="btn btn-peachy w-100 mb-3"
                >
                  <img src="img/google.png" alt="Google" width="24" height="24" />
                  <span>Sign up with Google</span>
                </button>

                {/* Login */}
                <p className="text-center mt-3 text-dark-brownish-gray">
                  Already have an account?{" "}
                  <a
                    href="#"
                    className="fw-semibold text-decoration-none"
                    style={{ color: "var(--peachy-highlight)" }}
                  >
                    Login
                  </a>
                </p>
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Signup;
