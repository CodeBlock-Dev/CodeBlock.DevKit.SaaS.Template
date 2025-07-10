// DOM Content Loaded
document.addEventListener('DOMContentLoaded', function() {
    // Initialize all functionality
    initMobileMenu();
    initVideoModals();
    initSmoothScrolling();
    initNavbarScroll();
    initModernAnimations();
    initLanguageSelector();
    initModernInteractions();
});

// Mobile Menu Functionality
function initMobileMenu() {
    const mobileToggle = document.querySelector('.mobile-menu-toggle');
    const navLinks = document.querySelector('.nav-links');
    
    if (mobileToggle) {
        mobileToggle.addEventListener('click', function() {
            mobileToggle.classList.toggle('active');
            navLinks.classList.toggle('active');
        });
    }
}

// Video Modal Functionality
function initVideoModals() {
    const modal = document.getElementById('videoModal');
    const videoFrame = document.getElementById('videoFrame');
    const closeBtn = document.querySelector('.video-close');
    
    // Close modal function
    function closeModal() {
        modal.classList.remove('show');
        setTimeout(() => {
            modal.style.display = 'none';
            videoFrame.src = '';
            videoFrame.classList.remove('loaded');
            const videoContainer = videoFrame.parentElement;
            videoContainer.classList.remove('video-loaded');
            document.body.style.overflow = 'auto';
        }, 300);
    }
    
    // Close button event
    if (closeBtn) {
        closeBtn.addEventListener('click', closeModal);
    }
    
    // Close on outside click
    modal?.addEventListener('click', function(e) {
        if (e.target === modal) {
            closeModal();
        }
    });
    
    // Close on escape key
    document.addEventListener('keydown', function(e) {
        if (e.key === 'Escape' && modal.style.display === 'block') {
            closeModal();
        }
    });
    
    // Global function for video playback
    window.playVideo = function(button) {
        const videoUrl = button.getAttribute('data-video-url');
        if (videoUrl) {
            openVideoModal(videoUrl);
        }
    };
    
    function openVideoModal(videoUrl) {
        // Show modal first
        modal.style.display = 'block';
        document.body.style.overflow = 'hidden';
        
        // Reset video container state
        const videoContainer = videoFrame.parentElement;
        videoContainer.classList.remove('video-loaded');
        videoFrame.classList.remove('loaded');
        
        // Trigger animation after a small delay
        setTimeout(() => {
            modal.classList.add('show');
        }, 10);
        
        // Load video after modal is visible
        setTimeout(() => {
            videoFrame.src = videoUrl;
            
            // Add loaded class when iframe loads
            videoFrame.onload = function() {
                videoFrame.classList.add('loaded');
                videoContainer.classList.add('video-loaded');
            };
        }, 300);
    }
}

// Smooth Scrolling for Navigation Links
function initSmoothScrolling() {
    const navLinks = document.querySelectorAll('a[href^="#"]');
    
    navLinks.forEach(link => {
        link.addEventListener('click', function(e) {
            const href = this.getAttribute('href');
            
            // Skip if it's just a hash
            if (href === '#') return;
            
            e.preventDefault();
            
            const targetId = href.substring(1);
            const targetElement = document.getElementById(targetId);
            
            if (targetElement) {
                const offsetTop = targetElement.offsetTop - 120; // Account for floating navbar
                
                window.scrollTo({
                    top: offsetTop,
                    behavior: 'smooth'
                });
            }
        });
    });
}

// Modern Navbar Scroll Effect
function initNavbarScroll() {
    const navbar = document.querySelector('.navbar');
    const heroSection = document.querySelector('.hero');
    let lastScrollTop = 0;
    let ticking = false;
    
    function updateNavbar() {
        const scrollTop = window.pageYOffset || document.documentElement.scrollTop;
        const heroHeight = heroSection ? heroSection.offsetHeight : window.innerHeight;
        
        // Add/remove scrolled class for styling
        if (scrollTop > 50) {
            navbar.classList.add('scrolled');
        } else {
            navbar.classList.remove('scrolled');
        }
        
        // Hide/show navbar logic:
        // - Hide when scrolling down past 200px
        // - Show when scrolling up AND still within hero section area
        // - Hide when outside hero section regardless of scroll direction
        if (scrollTop > lastScrollTop && scrollTop > 200) {
            // Scrolling down - hide navbar
            navbar.classList.add('hidden');
        } else if (scrollTop < lastScrollTop && scrollTop <= heroHeight) {
            // Scrolling up and within hero section - show navbar
            navbar.classList.remove('hidden');
        } else if (scrollTop > heroHeight) {
            // Outside hero section - keep hidden
            navbar.classList.add('hidden');
        }
        
        lastScrollTop = scrollTop;
        ticking = false;
    }
    
    // Call updateNavbar immediately on page load to set initial state
    updateNavbar();
    
    window.addEventListener('scroll', function() {
        if (!ticking) {
            requestAnimationFrame(updateNavbar);
            ticking = true;
        }
    });
}

// Modern Animation System
function initModernAnimations() {
    // Intersection Observer for slide-in animations
    const observerOptions = {
        threshold: 0.15,
        rootMargin: '0px 0px -50px 0px'
    };
    
    const observer = new IntersectionObserver(function(entries) {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('animate-in');
                
                // Trigger staggered animations for children if they exist
                const children = entry.target.querySelectorAll('.animate-ready');
                children.forEach((child, index) => {
                    setTimeout(() => {
                        child.classList.add('animate-in');
                    }, index * 80);
                });
            }
        });
    }, observerOptions);
    
    // Observe only section titles and subtitles
    const animatedElements = document.querySelectorAll('.section-title, .section-subtitle');
    animatedElements.forEach(element => {
        element.classList.add('animate-ready');
        observer.observe(element);
    });
    
    // Parallax effect for hero dots
    window.addEventListener('scroll', function() {
        const scrolled = window.pageYOffset;
        const hero = document.querySelector('.hero');
        if (hero && scrolled < window.innerHeight) {
            const speed = scrolled * 0.5;
            hero.style.transform = `translateY(${speed}px)`;
        }
    });
}

// Modern Interactions
function initModernInteractions() {
    // Add hover effects to interactive elements
    const buttons = document.querySelectorAll('.btn, .cta-btn, .cta-primary, .demo-btn, .pricing-cta');
    buttons.forEach(button => {
        button.addEventListener('mouseenter', function() {
            this.style.transform = 'translateY(-2px)';
        });
        
        button.addEventListener('mouseleave', function() {
            this.style.transform = 'translateY(0)';
        });
    });
    
    // Add ripple effect to buttons
    addRippleEffect();
    
    // Initialize loading states
    initLoadingStates();
    
    // Add modern focus management
    initFocusManagement();
}

// Add ripple effect to buttons
function addRippleEffect() {
    const buttons = document.querySelectorAll('.btn, .cta-btn, .cta-primary, .demo-btn, .pricing-cta');
    
    buttons.forEach(button => {
        button.addEventListener('click', function(e) {
            const ripple = document.createElement('span');
            const rect = this.getBoundingClientRect();
            const size = Math.max(rect.width, rect.height);
            const x = e.clientX - rect.left - size / 2;
            const y = e.clientY - rect.top - size / 2;
            
            ripple.style.width = ripple.style.height = size + 'px';
            ripple.style.left = x + 'px';
            ripple.style.top = y + 'px';
            ripple.classList.add('ripple');
            
            // Add ripple styles if not exist
            if (!document.querySelector('.ripple-styles')) {
                const style = document.createElement('style');
                style.className = 'ripple-styles';
                style.textContent = `
                    .ripple {
                        position: absolute;
                        border-radius: 50%;
                        background: rgba(255, 255, 255, 0.3);
                        transform: scale(0);
                        animation: ripple-animation 0.6s ease-out;
                        pointer-events: none;
                    }
                    @keyframes ripple-animation {
                        to {
                            transform: scale(2);
                            opacity: 0;
                        }
                    }
                    .btn, .cta-btn, .cta-primary, .demo-btn, .pricing-cta {
                        position: relative;
                        overflow: hidden;
                    }
                `;
                document.head.appendChild(style);
            }
            
            this.appendChild(ripple);
            
            setTimeout(() => {
                ripple.remove();
            }, 600);
        });
    });
}

// Initialize loading states
function initLoadingStates() {
    const forms = document.querySelectorAll('form');
    forms.forEach(form => {
        form.addEventListener('submit', function(e) {
            const submitBtn = this.querySelector('[type="submit"]');
            if (submitBtn) {
                submitBtn.innerHTML = '<i class="bi bi-arrow-repeat spinning"></i> Loading...';
                submitBtn.disabled = true;
            }
        });
    });
}

// Modern focus management
function initFocusManagement() {
    // Add focus-visible polyfill behavior
    document.addEventListener('keydown', function(e) {
        if (e.key === 'Tab') {
            document.body.classList.add('using-keyboard');
        }
    });
    
    document.addEventListener('mousedown', function() {
        document.body.classList.remove('using-keyboard');
    });
    
    // Add focus styles
    if (!document.querySelector('.focus-styles')) {
        const style = document.createElement('style');
        style.className = 'focus-styles';
        style.textContent = `
            .using-keyboard *:focus {
                outline: 2px solid var(--primary);
                outline-offset: 2px;
            }
            :not(.using-keyboard) *:focus {
                outline: none;
            }
            .spinning {
                animation: spin 1s linear infinite;
            }
            @keyframes spin {
                from { transform: rotate(0deg); }
                to { transform: rotate(360deg); }
            }
        `;
        document.head.appendChild(style);
    }
}

// Language Selector Functionality
function initLanguageSelector() {
    const languageSelect = document.querySelector('.language-select');
    
    if (languageSelect) {
        languageSelect.addEventListener('change', function() {
            const selectedLang = this.value;
            
            // Simple language switching logic
            // In a real application, you would implement proper i18n
            const translations = {
                'en': {
                    title: 'CodeBlock Dev Kit - SaaS Development Kit for Entrepreneurs',
                    subtitle: 'SaaS Development Kit for Entrepreneurs'
                },
                'ar': {
                    title: 'مجموعة أدوات كود بلوك للمطورين - مجموعة تطوير SaaS لرجال الأعمال',
                    subtitle: 'مجموعة تطوير SaaS لرجال الأعمال'
                },
                'es': {
                    title: 'Kit de Desarrollo CodeBlock - Kit de Desarrollo SaaS para Emprendedores',
                    subtitle: 'Kit de Desarrollo SaaS para Emprendedores'
                },
                'fr': {
                    title: 'Kit de Développement CodeBlock - Kit de Développement SaaS pour Entrepreneurs',
                    subtitle: 'Kit de Développement SaaS pour Entrepreneurs'
                },
                'de': {
                    title: 'CodeBlock Entwicklungskit - SaaS Entwicklungskit für Unternehmer',
                    subtitle: 'SaaS Entwicklungskit für Unternehmer'
                }
            };
            
            if (translations[selectedLang]) {
                document.title = translations[selectedLang].title;
                const subtitle = document.querySelector('.subtitle');
                if (subtitle) {
                    subtitle.textContent = translations[selectedLang].subtitle;
                }
                
                // Set RTL for Arabic
                if (selectedLang === 'ar') {
                    document.documentElement.setAttribute('dir', 'rtl');
                    document.body.style.fontFamily = '"IBM Plex Sans Arabic", "Inter", sans-serif';
                } else {
                    document.documentElement.setAttribute('dir', 'ltr');
                    document.body.style.fontFamily = '"Inter", -apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, sans-serif';
                }
            }
        });
    }
}

// Utility Functions
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Performance optimized scroll handler
const optimizedScrollHandler = debounce(function() {
    // Any additional scroll-based functionality can go here
}, 16); // ~60fps

window.addEventListener('scroll', optimizedScrollHandler);

// Modern lazy loading for images
function initLazyLoading() {
    if ('IntersectionObserver' in window) {
    const imageObserver = new IntersectionObserver((entries, observer) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const img = entry.target;
                img.src = img.dataset.src;
                img.classList.remove('lazy');
                    imageObserver.unobserve(img);
            }
        });
    });
    
        const lazyImages = document.querySelectorAll('img[data-src]');
        lazyImages.forEach(img => imageObserver.observe(img));
    }
}

// Initialize lazy loading
initLazyLoading();

// Add modern loading animation for the page
window.addEventListener('load', function() {
    document.body.classList.add('loaded');
    
    // Add loaded styles if not exist
    if (!document.querySelector('.loaded-styles')) {
        const style = document.createElement('style');
        style.className = 'loaded-styles';
        style.textContent = `
            body {
                transition: opacity 0.3s ease;
            }
            body:not(.loaded) {
                opacity: 0;
            }
            body.loaded {
        opacity: 1;
    }
        `;
        document.head.appendChild(style);
    }
});

// Modern error handling
window.addEventListener('error', function(e) {
    console.error('An error occurred:', e.error);
    // You could implement user-friendly error notifications here
});

// Service Worker registration for PWA capabilities (optional)
if ('serviceWorker' in navigator) {
    window.addEventListener('load', function() {
        // Uncomment if you want to add PWA capabilities
        // navigator.serviceWorker.register('/sw.js');
    });
}

// AI Network Animation Class
class AINetworkBackground {
    constructor(canvas) {
        this.canvas = canvas;
        this.ctx = canvas.getContext('2d');
        this.particles = [];
        this.connections = [];
        this.particleCount = 80; // Increased for more dots
        this.maxDistance = 140; // Slightly increased for more connections
        this.time = 0;
        
        this.init();
        this.animate();
        this.addEventListeners();
    }

    init() {
        this.resize();
        this.createParticles();
    }

    resize() {
        const rect = this.canvas.parentElement.getBoundingClientRect();
        this.canvas.width = rect.width;
        this.canvas.height = rect.height;
    }

    createParticles() {
        this.particles = [];
        for (let i = 0; i < this.particleCount; i++) {
            this.particles.push({
                x: Math.random() * this.canvas.width,
                y: Math.random() * this.canvas.height,
                baseX: Math.random() * this.canvas.width,
                baseY: Math.random() * this.canvas.height,
                radius: Math.random() * 2 + 0.8, // Smaller dots
                opacity: Math.random() * 0.4 + 0.2, // Reduced opacity for less brightness
                hue: Math.random() * 30 + 40, // Light muted yellow-beige range
                phase: Math.random() * Math.PI * 2,
                amplitude: Math.random() * 40 + 20, // Increased movement range
                frequency: Math.random() * 0.04 + 0.02, // Increased frequency for more speed
                orbitRadius: Math.random() * 80 + 40 // Larger orbit radius
            });
        }
    }

    updateParticles() {
        this.time += 0.03; // Increased from 0.015 for more speed
        
        this.particles.forEach((particle, index) => {
            // Wave-like movement pattern
            const waveX = Math.sin(this.time * particle.frequency + particle.phase) * particle.amplitude;
            const waveY = Math.cos(this.time * particle.frequency * 0.7 + particle.phase) * particle.amplitude * 0.6;
            
            // Circular orbital movement - faster
            const orbitX = Math.cos(this.time * 0.6 + index * 0.1) * particle.orbitRadius * 0.3; // Increased from 0.3
            const orbitY = Math.sin(this.time * 0.5 + index * 0.1) * particle.orbitRadius * 0.2; // Increased from 0.25
            
            // Combine movements
            particle.x = particle.baseX + waveX + orbitX;
            particle.y = particle.baseY + waveY + orbitY;
            
            // Keep particles in bounds with wrapping
            if (particle.x < -30) {
                particle.baseX = this.canvas.width + 30;
            } else if (particle.x > this.canvas.width + 30) {
                particle.baseX = -30;
            }
            
            if (particle.y < -30) {
                particle.baseY = this.canvas.height + 30;
            } else if (particle.y > this.canvas.height + 30) {
                particle.baseY = -30;
            }
            
            // Subtle opacity animation
            particle.opacity = 0.3 + Math.sin(this.time * 4 + particle.phase) * 0.2; // Reduced brightness
        });
    }

    drawParticles() {
        this.particles.forEach(particle => {
            this.ctx.beginPath();
            this.ctx.arc(particle.x, particle.y, particle.radius, 0, Math.PI * 2);
            this.ctx.fillStyle = `hsla(${particle.hue}, 25%, 70%, ${particle.opacity})`; // Light muted colors
            this.ctx.fill();
            
            // Subtle glow effect
            this.ctx.shadowBlur = 6; // Reduced glow
            this.ctx.shadowColor = `hsla(${particle.hue}, 25%, 70%, 0.3)`; // Light muted glow
            this.ctx.fill();
            this.ctx.shadowBlur = 0;
        });
    }

    drawConnections() {
        for (let i = 0; i < this.particles.length; i++) {
            for (let j = i + 1; j < this.particles.length; j++) {
                const p1 = this.particles[i];
                const p2 = this.particles[j];
                const dx = p1.x - p2.x;
                const dy = p1.y - p2.y;
                const distance = Math.sqrt(dx * dx + dy * dy);

                if (distance < this.maxDistance) {
                    const opacity = (1 - distance / this.maxDistance) * 0.2; // Reduced opacity for less brightness
                    this.ctx.beginPath();
                    this.ctx.moveTo(p1.x, p1.y);
                    this.ctx.lineTo(p2.x, p2.y);
                    this.ctx.strokeStyle = `rgba(180, 170, 160, ${opacity})`; // Light muted connection lines
                    this.ctx.lineWidth = 1;
                    this.ctx.stroke();
                }
            }
        }
    }

    animate() {
        this.ctx.clearRect(0, 0, this.canvas.width, this.canvas.height);
        
        this.updateParticles();
        this.drawConnections();
        this.drawParticles();

        requestAnimationFrame(() => this.animate());
    }

    addEventListeners() {
        window.addEventListener('resize', () => {
            this.resize();
            this.createParticles();
        });
    }
}

// Initialize AI Network Animation
function initAINetworkAnimation() {
    const canvas = document.getElementById('aiNetworkCanvas');
    if (canvas) {
        new AINetworkBackground(canvas);
    }
}

// Initialize AI Network Animation when DOM is loaded
document.addEventListener('DOMContentLoaded', initAINetworkAnimation);

// Demo Carousel Caption Animation
function initDemoCarouselCaptions() {
    const carousel = document.getElementById('demoCarousel');
    if (!carousel) return;

    let captionTimeout;
    let hideTimeout;

    // Function to show caption with delay
    function showCaption(caption) {
        if (captionTimeout) clearTimeout(captionTimeout);
        if (hideTimeout) clearTimeout(hideTimeout);
        
        // Hide caption initially
        caption.classList.remove('show');
        caption.classList.add('hide');
        
        // Show caption after image transition completes
        captionTimeout = setTimeout(() => {
            caption.classList.remove('hide');
            caption.classList.add('show');
            
            // Auto-hide caption before next slide
            hideTimeout = setTimeout(() => {
                caption.classList.remove('show');
                caption.classList.add('hide');
            }, 2300);
        }, 100);
    }

    // Function to hide caption immediately
    function hideCaption(caption) {
        if (captionTimeout) clearTimeout(captionTimeout);
        if (hideTimeout) clearTimeout(hideTimeout);
        caption.classList.remove('show');
        caption.classList.add('hide');
    }

    // Handle slide events
    carousel.addEventListener('slide.bs.carousel', function(e) {
        // Hide current caption when starting to slide
        const currentCaption = carousel.querySelector('.carousel-item.active .carousel-caption-custom');
        if (currentCaption) {
            hideCaption(currentCaption);
        }
    });

    carousel.addEventListener('slid.bs.carousel', function(e) {
        // Show new caption after slide is complete
        const newCaption = carousel.querySelector('.carousel-item.active .carousel-caption-custom');
        if (newCaption) {
            showCaption(newCaption);
        }
    });

    // Show first caption on page load
    setTimeout(() => {
        const firstCaption = carousel.querySelector('.carousel-item.active .carousel-caption-custom');
        if (firstCaption) {
            showCaption(firstCaption);
        }
    }, 300);
}

// Initialize demo carousel captions
document.addEventListener('DOMContentLoaded', initDemoCarouselCaptions);
