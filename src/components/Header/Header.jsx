import React, { useState } from 'react';
import { Link } from 'react-router-dom'; // استيراد مكون Link
import './Header.css'

function Navbar(){
  const [isNavCollapsed, setIsNavCollapsed] = useState(true);

  const handleNavCollapse = () => setIsNavCollapsed(!isNavCollapsed);

  return (
    <header className="sticky-top py-3 custom-header ">
      <div className="container">
        <nav className="navbar navbar-expand-md navbar-light p-0">
          {/* الشعار والاسم */}
          <Link className="navbar-brand d-flex align-items-center" to="/">
            <img src="/img/logo.png" alt="WeiaCrafts Logo" className="logo-img me-2" />
            <span className="brand-name">WeiaCrafts</span>
          </Link>
          
          {/* زر القائمة للشاشات الصغيرة */}
          <button 
            className="navbar-toggler" 
            type="button" 
            data-bs-toggle="collapse" 
            data-bs-target="#navbarNav" 
            aria-controls="navbarNav" 
            aria-expanded={!isNavCollapsed ? true : false} 
            aria-label="Toggle navigation"
            onClick={handleNavCollapse}
          >
            <span className="navbar-toggler-icon"></span>
          </button>
          
          {/* عناصر القائمة */}
          <div className={`${isNavCollapsed ? 'collapse' : ''} navbar-collapse justify-content-between`} id="navbarNav">
            <ul className="navbar-nav mx-auto">
              <li className="nav-item">
                <Link className="nav-link" to="/HomePage">Home</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/courses">Courses</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/weiastore">WeiaStore</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/weiakids">WeiaKids</Link>
              </li>
              <li className="nav-item">
                <Link className="nav-link" to="/pricing">Pricing</Link>
              </li>
            </ul>
            
            {/* أزرار تسجيل الدخول والاشتراك */}
            <div className="d-flex auth-buttons">
              <Link to="/login" className="btn btn-link login-btn">Login</Link>
              <Link to="/signup" className="btn btn-link signup-btn">Sign Up</Link>
            </div>
          </div>
        </nav>
      </div>
    </header>
  );
}

export default Navbar;
