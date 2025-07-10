// Modern SaaS Website JavaScript - Super Simple Animations
document.addEventListener('DOMContentLoaded', function() {
    
    // Simple animation checker - runs on every scroll
    function checkAnimations() {
        const elements = document.querySelectorAll('.fade-in, .slide-in-left, .slide-in-right, .scale-in');
        
        elements.forEach(el => {
            // Add animate class if not already added
            if (!el.classList.contains('animate')) {
                el.classList.add('animate');
            }
            
            // Check if element is in viewport
            const rect = el.getBoundingClientRect();
            const windowHeight = window.innerHeight;
            const isInViewport = rect.top < windowHeight - 100 && rect.bottom > 0;
            
            // Add or remove visible class based on viewport
            if (isInViewport) {
                el.classList.add('visible');
            }
        });
    }
    
    // Navbar scroll effect
    function updateNavbar() {
        const navbar = document.querySelector('.navbar');
        if (navbar) {
            if (window.scrollY > 50) {
                navbar.classList.add('scrolled');
            } else {
                navbar.classList.remove('scrolled');
            }
        }
    }
    
    // Parallax effect
    function updateParallax() {
        const scrolled = window.pageYOffset;
        const shapes = document.querySelectorAll('.shape');
        shapes.forEach((shape, index) => {
            const speed = (index + 1) * 0.02;
            const yPos = scrolled * speed;
            shape.style.transform = `translate3d(0, ${yPos}px, 0)`;
        });
    }
    
    // Combined scroll handler
    function handleScroll() {
        checkAnimations();
        updateNavbar();
        updateParallax();
    }

    // Smooth scrolling for anchor links (exclude dropdown toggles)
    document.querySelectorAll('a[href^="#"]:not(.dropdown-toggle):not([data-bs-toggle])').forEach(anchor => {
        anchor.addEventListener('click', function (e) {
            e.preventDefault();
            const target = document.querySelector(this.getAttribute('href'));
            if (target) {
                const offsetTop = target.offsetTop - 80;
                window.scrollTo({
                    top: offsetTop,
                    behavior: 'smooth'
                });
            }
        });
    });

    // Button hover effects
    function addHoverEffects() {
        document.querySelectorAll('.btn-primary-gradient, .btn-outline-primary-gradient').forEach(button => {
            button.addEventListener('mouseenter', function() {
                this.style.transform = 'translateY(-3px) scale(1.02)';
                this.style.transition = 'all 0.3s ease';
            });
            
            button.addEventListener('mouseleave', function() {
                this.style.transform = 'translateY(0) scale(1)';
            });
        });

        document.querySelectorAll('.feature-card, .pricing-card, .testimonial-card').forEach(card => {
            card.style.transition = 'all 0.4s cubic-bezier(0.4, 0, 0.2, 1)';
            
            card.addEventListener('mouseenter', function() {
                this.style.transform = 'translateY(-8px) scale(1.01)';
                this.style.boxShadow = '0 15px 35px rgba(0, 0, 0, 0.2)';
            });
            
            card.addEventListener('mouseleave', function() {
                this.style.transform = 'translateY(0) scale(1)';
                this.style.boxShadow = '';
            });
        });
    }

    // Initialize everything immediately
    addHoverEffects();
    
    // Run animations immediately
    checkAnimations();
    updateNavbar();
    updateParallax();
    
    // Add scroll listener that runs on every scroll
    window.addEventListener('scroll', handleScroll, { passive: true });
    
    // Also run on window load and resize
    window.addEventListener('load', checkAnimations);
    window.addEventListener('resize', checkAnimations);
    
    // Debug function
    window.triggerAnimations = function() {
        document.querySelectorAll('.fade-in, .slide-in-left, .slide-in-right, .scale-in').forEach(el => {
            el.classList.add('animate', 'visible');
        });
    };
});

