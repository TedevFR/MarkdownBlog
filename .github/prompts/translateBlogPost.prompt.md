---
name: "TranslateBlogPost"
description: "Traduction d'un article de blog"
---

# Etapes à suivre
- Tu devrais avoir un fichier markdown en contexte qui sera la base sur laquelle tu vas travailler. Si ce n'est pas le cas, ne fais rien et indique-le.
- Dans le front matter en yaml, traduis le champ title.fr et écrit le résultat dans le champ title.en
- Dans le front matter en yaml, traduis le champ excerpt.fr et écrit le résultat dans le champ excerpt.en
- Traduis le contenu de l'article de blog du français vers l'anglais et écris le résultat en fin de fichier, après le contenu en français. Entre les deux versions, ajoute une ligne avec uniquement `<!-- lang:en -->` pour séparer les deux langues.

# Contenu du fichier markdown après traduction
```
---
title:
  fr: "Titre en francais"
  en: "Title in english"
date: 2026-01-22
excerpt:
  fr: "Résumé de l'article en francais"
  en: "Summary of the article in english"
tags:
  - tag
---

# Nouvel article de blog
Ceci est un article en français.

<!-- lang:en -->

# New blog post
This is an article in English.
```

# Instructions supplémentaires
- Tu n'as pas besoin de lire les fichiers du projet ni de comprendre ce qu'il fait, comment ni pourquoi.
- Concentre-toi uniquement sur la traduction de l'article en suivant les étapes ci-dessus.