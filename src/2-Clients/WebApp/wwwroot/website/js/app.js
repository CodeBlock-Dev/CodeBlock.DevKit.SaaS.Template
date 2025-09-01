// Modern SaaS Website JavaScript - Super Simple Animations

document.addEventListener('DOMContentLoaded', function() {
    
    // Handle navigation menu anchor link clicks using data attribute
    function setupScrollToSectionLinks() {
        const scrollLinks = document.querySelectorAll('.nav-link[data-scroll-to-section="true"]');
        
        scrollLinks.forEach(link => {
            link.addEventListener('click', function(e) {
                e.preventDefault();
                const href = this.getAttribute('href');
                
                // Skip empty hash
                if (href === '#') return;
                
                // Update URL
                window.location.hash = href;
                
                // Scroll to element
                const target = document.querySelector(href);
                if (target) {
                    target.scrollIntoView({ behavior: 'smooth' });
                }
            });
        });
    }
    
    // Setup scroll links when DOM is ready
    setupScrollToSectionLinks();
    
    // Single Navigation Menu Setup - Clones nav items to offcanvas
    window.setupSingleNavMenu = function() {
        const singleNavMenu = document.getElementById('singleNavMenu');
        const offcanvasNavMenu = document.getElementById('offcanvasNavMenu');
        
        if (singleNavMenu && offcanvasNavMenu) {
            // Clone the navigation items
            const navItems = singleNavMenu.querySelectorAll('.nav-item');
            
            navItems.forEach(item => {
                // Skip language selector for mobile (it's already in the header)
                if (item.classList.contains('language-selector-desktop')) {
                    return;
                }
                
                const clonedItem = item.cloneNode(true);
                
                // Add mobile-specific classes
                if (clonedItem.classList.contains('dashboard-btn')) {
                    clonedItem.classList.add('mt-3');
                    const btn = clonedItem.querySelector('.btn');
                    if (btn) {
                        btn.classList.add('w-100', 'mb-2');
                    }
                }
                
                offcanvasNavMenu.appendChild(clonedItem);
            });
            
            // Setup scroll links for cloned items
            setupScrollToSectionLinks();
        }
    };
    
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

// to remove the white flash screen on page load by hiding the main-layout-wrapper tag and showing it after 1 second
// during this time the splash screen will be visible
window.addEventListener("DOMContentLoaded", () => {

    const websiteWrapper = document.getElementById('main-layout-wrapper');
    if (websiteWrapper) {
        // Hide immediately
        websiteWrapper.style.display = "none";

        // Remove style after 1 second
        setTimeout(() => {
            websiteWrapper.removeAttribute('style');
        }, 1000);
    }
});