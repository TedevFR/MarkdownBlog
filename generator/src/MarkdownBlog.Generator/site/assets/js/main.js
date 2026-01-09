// Base JS placeholder (T004)
// Keep core content functional without JS.

// Theme management
(function() {
  // Initialize theme immediately to prevent flash
  const getStoredTheme = () => localStorage.getItem('theme');
  const getSystemTheme = () => window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
  const getPreferredTheme = () => getStoredTheme() || getSystemTheme();
  
  // Set theme on HTML element before page renders
  const theme = getPreferredTheme();
  document.documentElement.setAttribute('data-theme', theme);
  
  // Update Highlight.js theme immediately
  updateHighlightTheme(theme);
  
  function updateHighlightTheme(theme) {
    const lightLink = document.querySelector('link[href*="github.min.css"]');
    const darkLink = document.querySelector('link[href*="github-dark.min.css"]');
    
    if (lightLink && darkLink) {
      if (theme === 'dark') {
        lightLink.media = 'not all';
        darkLink.media = 'all';
      } else {
        lightLink.media = 'all';
        darkLink.media = 'not all';
      }
    }
  }
})();

// Syntax highlighting initialization
document.addEventListener('DOMContentLoaded', () => {
  // Initialize Highlight.js for syntax highlighting
  document.querySelectorAll('code[class^="language-"]').forEach(el => {
    hljs.highlightElement(el);
  });

  // Theme toggle functionality
  const themeToggle = document.querySelector('.theme-toggle');
  
  if (themeToggle) {
    const getStoredTheme = () => localStorage.getItem('theme');
    const getSystemTheme = () => window.matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light';
    const getCurrentTheme = () => document.documentElement.getAttribute('data-theme') || getSystemTheme();
    
    const updateHighlightTheme = (theme) => {
      const lightLink = document.querySelector('link[href*="github.min.css"]');
      const darkLink = document.querySelector('link[href*="github-dark.min.css"]');
      
      if (lightLink && darkLink) {
        if (theme === 'dark') {
          lightLink.media = 'not all';
          darkLink.media = 'all';
        } else {
          lightLink.media = 'all';
          darkLink.media = 'not all';
        }
      }
    };
    
    const setTheme = (theme) => {
      document.documentElement.setAttribute('data-theme', theme);
      localStorage.setItem('theme', theme);
      updateThemeButton(theme);
      updateHighlightTheme(theme);
    };
    
    const updateThemeButton = (theme) => {
      const isDark = theme === 'dark';
      themeToggle.setAttribute('aria-pressed', isDark ? 'true' : 'false');
      themeToggle.setAttribute('aria-label', isDark ? 'Switch to light mode' : 'Switch to dark mode');
    };
    
    // Initialize button state
    updateThemeButton(getCurrentTheme());
    
    // Toggle theme on button click
    themeToggle.addEventListener('click', () => {
      const currentTheme = getCurrentTheme();
      const newTheme = currentTheme === 'dark' ? 'light' : 'dark';
      setTheme(newTheme);
    });
    
    // Listen for system theme changes
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (e) => {
      // Only auto-switch if user hasn't set a preference
      if (!getStoredTheme()) {
        const newTheme = e.matches ? 'dark' : 'light';
        document.documentElement.setAttribute('data-theme', newTheme);
        updateThemeButton(newTheme);
        updateHighlightTheme(newTheme);
      }
    });
  }

  // Code block copy functionality
  const copyButtons = document.querySelectorAll('.code-block-copy');
  
  copyButtons.forEach(button => {
    button.addEventListener('click', async () => {
      const codeBlock = button.closest('.code-block-container');
      const codeElement = codeBlock.querySelector('code');
      
      if (!codeElement) return;
      
      const code = codeElement.textContent;
      
      try {
        await navigator.clipboard.writeText(code);
        
        // Show feedback
        const originalHTML = button.innerHTML;
        button.innerHTML = '<svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M13.5 4L6 11.5L2.5 8" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/></svg>';
        button.style.color = '#2da44e';
        
        setTimeout(() => {
          button.innerHTML = originalHTML;
          button.style.color = '';
        }, 2000);
      } catch (err) {
        // Fallback for older browsers
        const textArea = document.createElement('textarea');
        textArea.value = code;
        textArea.style.position = 'fixed';
        textArea.style.left = '-999999px';
        document.body.appendChild(textArea);
        textArea.select();
        
        try {
          document.execCommand('copy');
          
          const originalHTML = button.innerHTML;
          button.innerHTML = '<svg width="16" height="16" viewBox="0 0 16 16" fill="none" xmlns="http://www.w3.org/2000/svg"><path d="M13.5 4L6 11.5L2.5 8" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/></svg>';
          button.style.color = '#2da44e';
          
          setTimeout(() => {
            button.innerHTML = originalHTML;
            button.style.color = '';
          }, 2000);
        } catch (copyErr) {
          console.error('Failed to copy code:', copyErr);
        }
        
        document.body.removeChild(textArea);
      }
    });
  });
});
