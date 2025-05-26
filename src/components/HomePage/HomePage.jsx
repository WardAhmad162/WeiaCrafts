import React from 'react';
import { Container, Row, Col, Button, Card, Image } from 'react-bootstrap';
// Assuming index.css is imported globally or in a parent component
// import './index.css'; // Or wherever your index.css is relative to this component

// Placeholder data - Replace with your actual data source
const trendingCourses = [
  {
    id: 1,
    title: 'Personalized Candles, beginner Techniques',
    trainer: 'Luna Darri',
    rating: 4.5,
    reviews: 10812,
    price: 9.99,
    oldPrice: 34.99,
    image: 'img/course-candle.jpg', // Replace with actual image path
  },
  {
    id: 2,
    title: 'Make your own furniture',
    trainer: 'Ibraheem Mohammad',
    rating: 4.0,
    reviews: 9520,
    price: 9.99,
    oldPrice: 34.99,
    image: 'img/course-furniture.jpg', // Replace with actual image path
  },
  {
    id: 3,
    title: 'Porcelain Personalized: Drinkware & Hotware',
    trainer: 'Luna Darri',
    rating: 4.6,
    reviews: 5812,
    price: 9.99,
    oldPrice: 34.99,
    image: 'img/course-porcelain.jpg', // Replace with actual image path
  },
  {
    id: 4,
    title: 'Custom your dream Resin decorations',
    trainer: 'Luna Darri',
    rating: 4.5,
    reviews: 15678,
    price: 9.99,
    oldPrice: 34.99,
    image: 'img/course-resin.jpg', // Replace with actual image path
  },
];

const popularTrainers = [
  {
    id: 1,
    name: 'Ibraheem Sami',
    specialty: 'Carpentry, Resin and Blacksmiths',
    rating: 4.7,
    image: 'img/trainer-1.jpg', // Replace with actual image path
  },
  {
    id: 2,
    name: 'Mohammad Elyas',
    specialty: 'Wood Work and Blacksmiths',
    rating: 4.7,
    image: 'img/trainer-2.jpg', // Replace with actual image path
  },
  {
    id: 3,
    name: 'Luna Darri',
    specialty: 'Porcelain, Candle Ceramics and Resin',
    rating: 4.6,
    image: 'img/trainer-3.jpg', // Replace with actual image path
  },
    {
    id: 4,
    name: 'Elysa Sabaneh',
    specialty: 'Cardboard, Crochet and Embroidery',
    rating: 4.9,
    image: 'img/trainer-4.jpg', // Replace with actual image path
  },
  {
    id: 5,
    name: 'Laila Salah',
    specialty: 'Jewelry design, Baking and Pottery',
    rating: 4.8,
    image: 'img/trainer-5.jpg', // Replace with actual image path
  },
  {
    id: 6,
    name: 'Lara Salameh',
    specialty: 'Cardboard, Backing and Traditional Embroidery',
    rating: 4.8,
    image: 'img/trainer-6.jpg', // Replace with actual image path
  },
];

// Helper component for star ratings
const StarRating = ({ rating }) => {
  const fullStars = Math.floor(rating);
  const halfStar = rating % 1 >= 0.5;
  const emptyStars = 5 - fullStars - (halfStar ? 1 : 0);
  return (
    <span className="text-warning">
      {[...Array(fullStars)].map((_, i) => <i key={`full-${i}`} className="bi bi-star-fill"></i>)}
      {halfStar && <i className="bi bi-star-half"></i>}
      {[...Array(emptyStars)].map((_, i) => <i key={`empty-${i}`} className="bi bi-star"></i>)}
    </span>
  );
};

function HomePage() {
  return (
    <div style={{ backgroundColor: 'var(--soft-beige)' }}>
      {/* Hero Section - Redesigned to show full image with text at bottom corner */}
      <Container fluid className="p-0 mb-5">
        {/* Added margin-top to create space between header and hero image */}
        <div style={{ marginTop: '2rem' }}>
          {/* Hero image container with relative positioning */}
          <div style={{ position: 'relative' }}>
            {/* Full width image - z-index: 1 (base layer) */}
            <img 
              src="img/hero-background.png" 
              alt="Hero background" 
              className="img-fluid w-100" 
              style={{ 
                display: 'block',
                position: 'relative',
                zIndex: 1
              }}
            />
            
            {/* Semi-transparent white overlay - z-index: 2 (middle layer) */}
            <div 
              style={{ 
                position: 'absolute', 
                top: 0, 
                left: 0, 
                width: '100%', 
                height: '100%', 
                backgroundColor: 'rgba(255, 255, 255, 0.3)', // Semi-transparent white overlay
                zIndex: 2 // Above the image but below the text
              }}
            ></div>
            
            {/* Text overlay positioned at bottom left - z-index: 3 (top layer) */}
            <div 
              style={{ 
                position: 'absolute', 
                bottom: '2rem', 
                left: '2rem',
                maxWidth: '500px',
                padding: '1rem',
                zIndex: 3 // Ensure text appears above the white overlay
              }}
            >
              <h2 className="text-muted-olive-green fw-bold mb-3">
                Building Opportunities Through Handcrafted Skills
              </h2>
              <p className="text-dark-brownish-gray mb-4">
                Let's turn your big dreams into something real. Together, we'll build the business you've always dreamt of.
              </p>
              <Button variant="" className="btn-peachy px-4 py-2">Sign Up</Button>
            </div>
          </div>
        </div>
      </Container>

      {/* Trending Courses Section */}
      <Container className="mb-5 py-4">
        <h2 className="text-center text-gradient fw-bold mb-3">Trending courses of the month</h2>
        <p className="text-center text-muted-olive-green mb-5 fs-5">
          Let's turn your big dreams into something real. Together, we'll build the business you've always dreamt of.
        </p>
        <Row xs={1} sm={2} lg={4} className="g-4">
          {trendingCourses.map((course) => (
            <Col key={course.id}>
              <Card className="h-100 border-0 shadow-sm" style={{ backgroundColor: 'var(--cards)', borderRadius: 'var(--card-border-radius)' }}>
                <Card.Img variant="top" src={course.image} style={{ borderTopLeftRadius: 'var(--card-border-radius)', borderTopRightRadius: 'var(--card-border-radius)' }} />
                <Card.Body className="d-flex flex-column">
                  <Card.Title className="text-dark-brownish-gray fw-semibold mb-1" style={{ minHeight: '3em' }}>{course.title}</Card.Title>
                  <Card.Text className="text-muted-olive-green mb-2">{course.trainer}</Card.Text>
                  <div className="d-flex align-items-center mb-2">
                    <span className="text-dark-brownish-gray fw-bold me-1">{course.rating.toFixed(1)}</span>
                    <StarRating rating={course.rating} />
                    <span className="text-muted-olive-green ms-1">({course.reviews.toLocaleString()})</span>
                  </div>
                  <div className="mt-auto">
                    <span className="text-danger fw-bold fs-5 me-2">${course.price.toFixed(2)}</span>
                    <span className="text-muted-olive-green text-decoration-line-through">${course.oldPrice.toFixed(2)}</span>
                  </div>
                </Card.Body>
              </Card>
            </Col>
          ))}
        </Row>
      </Container>

      {/* Most Popular Trainers Section */}
      <Container className="mb-5 py-4">
        <h2 className="text-center text-gradient fw-bold mb-3">Most Popular Trainers Of Year 2024</h2>
        <p className="text-center text-muted-olive-green mb-5 fs-5">
          To all Trainers thank you for joining our team. Thank you for sharing your knowledge.
        </p>
        {/* Adjust column numbers for different screen sizes as needed */}
        <Row xs={2} sm={3} md={4} lg={6} className="g-4 justify-content-center">
          {popularTrainers.map((trainer) => (
            <Col key={trainer.id} className="text-center">
              <Image src={trainer.image} roundedCircle fluid width={120} height={120} className="mb-2 shadow-sm" style={{ objectFit: 'cover' }} />
              <h5 className="text-dark-brownish-gray fw-semibold mb-1">{trainer.name}</h5>
              <p className="text-muted-olive-green mb-1" style={{ fontSize: '0.9rem' }}>{trainer.specialty}</p>
              <div className="d-flex align-items-center justify-content-center">
                 <span className="text-dark-brownish-gray fw-bold me-1">{trainer.rating.toFixed(1)}</span>
                 <StarRating rating={trainer.rating} />
              </div>
            </Col>
          ))}
        </Row>
      </Container>

      {/* Add Footer section here if needed */}

    </div>
  );
}

export default HomePage;
