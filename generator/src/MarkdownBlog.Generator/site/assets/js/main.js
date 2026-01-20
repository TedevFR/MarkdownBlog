// Base JS placeholder (T004)
// Keep core content functional without JS.

// i18n translations
const translations = {
  fr: {
    'nav.home': 'Accueil',
    'nav.posts': 'Tous les articles',
    'nav.about': '\u00C0 propos',
    'footer.copyright': 'Markdown Blog par Teddy Le Bras',
    'lang.switch': 'Passer en anglais',
    'home.hero.title': 'Salut, moi c\'est Teddy !',
    'home.hero.intro': 'Technical Leader et d\u00E9veloppeur C# avec plus de 17 ans d\'exp\u00E9rience, je partage ici mes d\u00E9couvertes sur le d\u00E9veloppement .NET, l\'architecture logicielle, le cloud et l\'intelligence artificielle.',
    'home.title': 'Derniers posts',
    'home.empty': 'Pas encore d\'article.',
    'home.empty.link': 'En savoir plus sur la page \u00C0 propos',
    'list.title': 'Tous les articles',
    'list.empty': 'Pas encore d\'articles.',
    'list.empty.link': 'En savoir plus sur la page \u00C0 propos',
    'about.title': '\u00C0 propos',
    'about.intro': 'Je suis <b>Teddy Le Bras</b>, Technical Leader et d\u00E9veloppeur C# avec plus de 17 ans d\'exp\u00E9rience dans la conception et le d\u00E9veloppement d\'applications logicielles complexes.',
    'about.career': 'Depuis le d\u00E9but de ma carri\u00E8re, je navigue entre passion pour la technique, go\u00FBt pour la r\u00E9solution de probl\u00E8mes et envie de transmettre. Aujourd\'hui, j\'interviens chez Fnac, o\u00F9 j\'accompagne une \u00E9quipe de d\u00E9veloppeurs dans la conception de services critiques, la modernisation d\'applications (.NET 4.8 vers .NET 10) et la migration vers le cloud Azure. Mon quotidien, c\'est autant du code que de l\'architecture, du mentoring, des revues de code, et des \u00E9changes avec les Product Owners, QA et DevOps pour faire avancer les projets dans le bon sens. J\'ai notamment travaill\u00E9 sur l\'API de pricing, capable d\'encaisser des pics \u00E0 1,2 million de requ\u00EAtes par minute.',
    'about.motivation': 'Ce qui me motive le plus, ce n\'est pas seulement de faire fonctionner un syst\u00E8me, mais de le rendre robuste, compr\u00E9hensible et durable : clean architecture, qualit\u00E9 de code, automatisation, r\u00E9duction de la dette technique, am\u00E9lioration continue. J\'accorde aussi beaucoup d\'importance \u00E0 la transmission : accompagner les d\u00E9veloppeurs, faire grandir les \u00E9quipes, partager les bonnes pratiques.',
    'about.blog.intro': 'En parall\u00E8le de mon activit\u00E9, je m\'int\u00E9resse de tr\u00E8s pr\u00E8s \u00E0 l\'intelligence artificielle et \u00E0 son impact sur notre m\u00E9tier. Je fais partie d\'une squad IA d\u00E9di\u00E9e \u00E0 la veille, \u00E0 la formation et \u00E0 la cr\u00E9ation d\'outils pour am\u00E9liorer la productivit\u00E9 des \u00E9quipes. Ce blog est justement n\u00E9 de cette envie : explorer, comprendre, tester, prendre du recul... et partager. Tu trouveras ici des articles autour de :',
    'about.blog.topic1': 'la conception et le d\u00E9veloppement logiciel (.NET, architecture, bonnes pratiques)',
    'about.blog.topic2': 'le cloud et la performance',
    'about.blog.topic3': 'les outils et la productivit\u00E9',
    'about.blog.topic4': 'l\'intelligence artificielle et l\'avenir du m\u00E9tier',
    'about.blog.topic5': 'et parfois, des r\u00E9flexions plus personnelles sur la tech et le travail',
    'post.published': 'Publi\u00E9 le',
    'error.404.title': 'Page non trouv\u00E9e',
    'error.404.message': 'La page que vous recherchez n\'existe pas.',
    'error.404.link': 'Retour \u00E0 l\'accueil'
  },
  en: {
    'nav.home': 'Home',
    'nav.posts': 'All Posts',
    'nav.about': 'About',
    'footer.copyright': 'Markdown Blog by Teddy Le Bras',
    'lang.switch': 'Switch to French',
    'home.hero.title': 'Hi, I\'m Teddy!',
    'home.hero.intro': 'Technical Leader and C# developer with over 17 years of experience, I share here my discoveries about .NET development, software architecture, cloud and artificial intelligence.',
    'home.title': 'Latest posts',
    'home.empty': 'No posts yet.',
    'home.empty.link': 'Learn more on the About page',
    'list.title': 'All Posts',
    'list.empty': 'No posts yet.',
    'list.empty.link': 'Learn more on the About page',
    'about.title': 'About',
    'about.intro': 'I am <b>Teddy Le Bras</b>, Technical Leader and C# developer with over 17 years of experience in designing and developing complex software applications.',
    'about.career': 'Since the beginning of my career, I have navigated between a passion for technology, a taste for problem-solving, and a desire to share knowledge. Today, I work at Fnac, where I support a team of developers in designing critical services, modernizing applications (.NET 4.8 to .NET 10), and migrating to Azure cloud. My daily work involves coding, architecture, mentoring, code reviews, and collaborating with Product Owners, QA, and DevOps to move projects forward. I have notably worked on the pricing API, capable of handling peaks of 1.2 million requests per minute.',
    'about.motivation': 'What motivates me most is not just making a system work, but making it robust, understandable, and sustainable: clean architecture, code quality, automation, technical debt reduction, and continuous improvement. I also place great importance on knowledge sharing: supporting developers, helping teams grow, and sharing best practices.',
    'about.blog.intro': 'Alongside my work, I am very interested in artificial intelligence and its impact on our profession. I am part of an AI squad dedicated to monitoring, training, and creating tools to improve team productivity. This blog was born from this desire: to explore, understand, test, step back... and share. Here you will find articles about:',
    'about.blog.topic1': 'software design and development (.NET, architecture, best practices)',
    'about.blog.topic2': 'cloud and performance',
    'about.blog.topic3': 'tools and productivity',
    'about.blog.topic4': 'artificial intelligence and the future of our profession',
    'about.blog.topic5': 'and sometimes, more personal reflections on tech and work',
    'post.published': 'Published on',
    'error.404.title': 'Page not found',
    'error.404.message': 'The page you requested could not be found.',
    'error.404.link': 'Go to home'
  }
};

// Language management
(function() {
  const getStoredLang = () => localStorage.getItem('lang');
  const getPreferredLang = () => getStoredLang() || 'fr';
  
  // Set language on HTML element before page renders
  const lang = getPreferredLang();
  document.documentElement.setAttribute('lang', lang);
})();

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

  // Language functions (available globally for initialization)
  const getStoredLang = () => localStorage.getItem('lang');
  const getCurrentLang = () => document.documentElement.getAttribute('lang') || 'fr';

  const translatePage = (lang) => {
    document.querySelectorAll('[data-i18n]').forEach(el => {
      const key = el.getAttribute('data-i18n');
      if (translations[lang] && translations[lang][key]) {
        // Use innerHTML to support HTML tags in translations (like <b>)
        el.innerHTML = translations[lang][key];
      }
    });
  };

  const toggleLocalizedContent = (lang) => {
    // Show/hide content based on language
    document.querySelectorAll('[data-i18n-content]').forEach(el => {
      const contentLang = el.getAttribute('data-i18n-content');
      if (contentLang.endsWith('-fr')) {
        el.style.display = lang === 'fr' ? '' : 'none';
      } else if (contentLang.endsWith('-en')) {
        el.style.display = lang === 'en' ? '' : 'none';
      }
    });
  };

  const formatDates = (lang) => {
    const locale = lang === 'fr' ? 'fr-FR' : 'en-US';
    const options = { year: 'numeric', month: 'long', day: 'numeric' };
    
    document.querySelectorAll('[data-i18n-date]').forEach(el => {
      const isoDate = el.getAttribute('data-i18n-date');
      if (isoDate) {
        const date = new Date(isoDate + 'T00:00:00');
        el.textContent = date.toLocaleDateString(locale, options);
      }
    });
  };

  // Always initialize language on page load
  const currentLang = getCurrentLang();
  translatePage(currentLang);
  toggleLocalizedContent(currentLang);
  formatDates(currentLang);

  // Language toggle functionality
  const langToggle = document.querySelector('.lang-toggle');
  
  if (langToggle) {
    const setLang = (lang) => {
      document.documentElement.setAttribute('lang', lang);
      localStorage.setItem('lang', lang);
      updateLangButton(lang);
      translatePage(lang);
      toggleLocalizedContent(lang);
      formatDates(lang);
    };
    
    const updateLangButton = (lang) => {
      const langLabel = langToggle.querySelector('.lang-label');
      if (langLabel) {
        langLabel.textContent = lang === 'fr' ? 'EN' : 'FR';
      }
      langToggle.setAttribute('aria-label', translations[lang]['lang.switch']);
    };

    // Initialize button state
    updateLangButton(currentLang);
    
    // Toggle language on button click
    langToggle.addEventListener('click', () => {
      const currentLang = getCurrentLang();
      const newLang = currentLang === 'fr' ? 'en' : 'fr';
      setLang(newLang);
    });
  }

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
