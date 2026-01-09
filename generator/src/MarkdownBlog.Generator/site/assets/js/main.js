// Base JS placeholder (T004)
// Keep core content functional without JS.

// Syntax highlighting initialization
document.addEventListener('DOMContentLoaded', () => {
  // Initialize Highlight.js for syntax highlighting
  document.querySelectorAll('code[class^="language-"]').forEach(el => {
    hljs.highlightElement(el);
  });

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
