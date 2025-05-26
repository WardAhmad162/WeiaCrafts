import React from 'react';
import './Footer.css';

const Footer = () => {
  return (
    <footer className="px-2 py-4 text-sm">
      <div className="container">
        <div className="row">
          
          {/* About Us */}
          <div className="col-12 col-md-6 col-lg-3 mb-3">
            <h4 className="footer-heading">About Us</h4>
            <ul className="list-unstyled footer-links">
              <li><a href="#">Rewards</a></li>
              <li><a href="#">Trainers</a></li>
              <li><a href="#">About WeiaCrafts</a></li>
              <li><a href="#">Meet our Family</a></li>
              <li><a href="#">Careers</a></li>
              <li><a href="#">Join as a Trainer</a></li>
            </ul>
          </div>
      
          {/* Help Center */}
          <div className="col-12 col-md-6 col-lg-3 mb-3">
            <h4 className="footer-heading">Help Center</h4>
            <ul className="list-unstyled footer-links">
              <li><a href="#">Gift Subscriptions</a></li>
              <li><a href="#">Gift Packages</a></li>
              <li><a href="#">Redeem Gift</a></li>
              <li><a href="#">FAQs</a></li>
              <li><a href="#">Customer Support</a></li>
              <li><a href="#">Terms of use</a></li>
              <li><a href="#">Privacy Policy</a></li>
              <li><a href="#">Community Guidelines</a></li>
            </ul>
          </div>
      
          {/* App Downloads */}
          <div className="col-12 col-md-6 col-lg-3 mb-3">
            <h4 className="footer-heading">Download the App</h4>
            <div className="app-downloads">
              <a href="#" className="d-flex align-items-center mb-2"> 
                <img src="/img/google-play.png" alt="Google Play" className="app-icon me-2" />
                <div className="d-flex flex-column">
                  <span className="app-text">Get it On</span>
                  <span className="app-name">Google Play</span>
                </div>
              </a>
              <a href="#" className="d-flex align-items-center mb-2"> 
                <img src="/img/app-store.png" alt="App Store" className="app-icon me-2" />
                <div className="d-flex flex-column">
                  <span className="app-text">Get it On</span>
                  <span className="app-name">App Store</span>
                </div>
              </a>
              <a href="#" className="d-flex align-items-center mb-2"> 
                <img src="/img/microsoft.png" alt="Microsoft" className="app-icon me-2" />
                <div className="d-flex flex-column">
                  <span className="app-text">Get it On</span>
                  <span className="app-name">Microsoft</span>
                </div>
              </a>
            </div>
          </div>
      
          {/* Social Media */}
          <div className="col-12 col-md-6 col-lg-3 mb-3">
            <div className="d-flex flex-column align-items-center">
              <h4 className="footer-heading">Subscribe now</h4>
              <div className="d-flex social-icons">
                <a href="#" className="me-2"><img src="/img/icon-facebook.png" alt="Facebook" className="social-icon" /></a>
                <a href="#" className="me-2"><img src="/img/icon-instagram.png" alt="Instagram" className="social-icon" /></a>
                <a href="#" className="me-2"><img src="/img/icon-youtube.png" alt="YouTube" className="social-icon" /></a>
              </div>
              <a href="#" className="rate-us mt-2">Rate Us</a>
            </div>
          </div>
        </div>
      </div>
      
      <div className="text-center mt-3 copyright">
        © 2025 WeiaCrafts, Jenin
      </div>
    </footer>
  );
};

export default Footer;
