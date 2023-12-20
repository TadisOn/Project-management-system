import React, { useState, useEffect } from 'react';
import './Carousel.css'; // Make sure to create this CSS file
import img1 from "../../Images/img.jpg";
import img2 from "../../Images/bmw.jpg";
import img3 from "../../Images/mersedes.jpg";

const images = [
    img1,
    img2,
    img3
  
];

const Carousel = () => {
    const [currentIndex, setCurrentIndex] = useState(0);
    const [prevIndex, setPrevIndex] = useState(null);
  
    useEffect(() => {
      const intervalId = setInterval(() => {
        // Set the previous index before updating the current index
        setPrevIndex(currentIndex);
        setCurrentIndex((prevIndex) => (prevIndex + 1) % images.length);
      }, 2000); // Change image every 10 seconds
  
      return () => clearInterval(intervalId); // Clear interval on component unmount
    }, [currentIndex]);
  
    return (
      <div className="carousel">
        {images.map((image, index) => (
          <img 
            key={image}
            src={image} 
            alt={`Slide ${index + 1}`}
            className={`
              carousel-image 
              ${index === currentIndex ? 'carousel-image-active' : ''}
              ${prevIndex === index ? 'carousel-image-exiting' : ''}
            `}
            onAnimationEnd={() => {
              // Handle the end of the exit animation
              if (prevIndex === index) {
                setPrevIndex(null);
              }
            }}
          />
        ))}
      </div>
    );
  };

export default Carousel;
