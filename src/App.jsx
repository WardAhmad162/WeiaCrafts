import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css'
import Header from './components/Header/Header'
import Signup from './components/SignUp/Signup'
import Footer from './components/Footer/Footer'
import Login from './components/Login/Login';
import HomePage from './components/HomePage/HomePage';
function App() {

  return (
    <>
       <Router>
      <div className="App">
        <Header />
        <main className="container my-4">
          <Routes>
            <Route path="/HomePage" element={<HomePage/>} />
            <Route path="/Login" element={<Login />} />
            <Route path="/Signup" element={<Signup />} />
            {/* يمكنك إضافة المزيد من المسارات هنا */}
          </Routes>
        </main>
        
        <Footer />
      </div>
    </Router>
    </>
  )
}

export default App
