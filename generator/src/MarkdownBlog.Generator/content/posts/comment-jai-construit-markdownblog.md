---
title: "MarkdownBlog et le Spec-Driven Development"
date: 2030-01-01
excerpt: "C'est quoi le Spec-Driven Development et comment ca m'a aidé à constuire ce blog."
cover: ../../assets/img/cover.svg
tags:
  - dotnet
  - MarkdownBlog
  - SDD
  - Spec-Driven Development
  - Spec Kit
---

## Un blog ##

Je voulais ouvrir un blog depuis des années ! Pour pleins de raisons.
La première c'est simplement de vérifier mon niveau de compréhension. Si je suis capable d'expliquer, c'est que c'est clair dans ma tête. Pour ne pas écrire d'anerie, je vais très certainement creuser d'avantage et approfondir mes connaissances. Enfin, dans un registre moins personnel, ca me permet de partager des découvertes, de provoquer des discussions et booster mon réseau en faisant moins les choses seul dans mon coin.

Très simplement, voici ce que je voulais:
- [Détenir mes mots](https://www.hanselman.com/blog/your-words-are-wasted)
- Pouvoir écrire sans me soucier du HTML
- Ne pas héberger moi même le blog
- Pouvoir changer d'hébergeur facilement
- Ne pas payer pour l'hébergement (si possible)

## MarkdownBlog ##

Ce qui m'a toujours freiné, c'est de devoir mettre en place le blog. Ecrire, oui, mais créer le blog... Pas le temps, toujours mieux à faire etc. Je suis sûr que c'est un sentiment assez répendu.

Pour mon plus grand bonheur, depuis quelques temps l'IA fait des gros progrès et me permet maintenant de me lancer dans la construction d'un blog sans y passer énormément de temps. Cerise sur le gateau, je transforme ça en opportunité pour jouer avec des outils d'IA générative.

Pour répondre à ce besoin, j'ai créé un projet sur mesure, **MarkdownBlog**, un générateur de blog statique en .NET 10: page d’accueil, index des articles, pages d’articles, et les bases SEO comme les tags Open Graph, les URLs canoniques, `robots.txt` et un `sitemap.xml`. Ca parse des fichiers markdown pour générer un blog statique. Ce projet est hébergé [sur GitHub](https://github.com/TedevFR/MarkdownBlog) et une petite GitHub Action publie le résultat grace à GitHub Pages.

Une fois le projet terminé, je n'aurais plus qu'à pousser un nouveau fichier markdown pour que le blog soit à jour. Parfait !

Il n'y a plus qu'à s'y mettre. Pour rendre ça amusant et formateur, j'ai décidé de le faire en Spec-Driven Development.

## Le Spec-Driven Development

Après le TDD et le BDD, le SDD (rien à voir avec un disque dur), le Spec-Driven Development.
L'idée c'est d'écrire les spécifications en premier, puis de générer le code à partir ça... C'est déjà ce qu'on fait vous allez me dire sauf que l'outil c'est le développeur. Alors, quelle est la différence ?
Le terme est encore nouveau, je vous conseil la lecteur de [cet article](https://martinfowler.com/articles/exploring-gen-ai/sdd-3-tools.html) pour comprendre que le sens n'est encore complètement défini. Je vais tenter ici de vous donner ma propre définition.

L'approche SDD met encore plus en avant les specifications en les faisant devenir ce qu'il y a de plus important dans le projet. Ce n'est plus le code qui est maintenu dans le repo mais bien les specifications. Le code est toujours le résultat final mais il n'a plus vraiment d'importance. Il est devenu jetable pour de bon, comme vos binaires en local après avoir débugé.
Tout ça est bien entendu possible dans un contexte plus ou moins futuriste dans lequel les humains n'écrivent plus de code mais que cette tache est confiée à une IA.

Quel est l'intérêt ?

Comme régulièrement en programmation, à encapsuler un concept complexe pour nous permettre de nous concentrer sur un niveau d'abstraction supérieur. Si on regarde l'histoire, on a commencé avec de la programmation très bas niveau. Coucou l'assembleur. Tout était long, il fallait tout faire nous même. Avec le temps sont apparus des langages de plus haut niveau, permettant d'oublier certains détails et de se concentrer sur des choses plus importantes comme l'architecture. Le SDD prétend être l'étape suivante en utilisant directement le langage humain. L'architecture et la connaissance même d'un langage de programmation pourrait devenir des détails d'implémentation. C'est l'IA qui gérera tout ça.

Plus concrètement, vous décidez de changer le langage dans lequel est fait votre application ? Changez votre spec (oui une spec peut aussi être technique !) et regénérez votre projet, c'est terminé. Bien sur l'IA d'aujourd'hui ne nous permet pas encore de faire ça mais ce sera problement possible un jour.

## Spec Kit ##

Je savais ce que je voulais mais seulement dans les grandes lignes. Je pensais qu'il allait falloir écrire toutes les spécifications de façon fastidieuse puis que Spec Kit allait faire tout le code à ma place. Pas du tout ! Spec Kit m'a permis de me lancer rapidement sans avoir tous les détails en tête. Spec Kit définit un workflow pour vous aider à générer tout ce dont il a besoin. Chaque étape est réalisée à l'aide de son propre prompt. Ces prompts sont installés lors de l'initialisation de Spec Kit. Je vais passer sur la phase d'installation mais tout est expliqué sur leur [GitHub](https://github.com/github/spec-kit). Regardons ensemble les étapes dans l'ordre.


### Constitution ###

La première chose à faire est d'établir la constitution, c'est à dire l'ensemble des règles concernant la qualité du code, les standards à respecter, les contraintes de performance etc. Si vous avez le syndrome de la page blanche ou pas envie de trop vous embéter pour un projet sans enjeux, vous pouvez demander à l'IA de générer une première version pour vous. Dans mon cas, je lui ai simplement demander de créer la constitution parfaite pour lui en expliquant très rapidement le principe de mon application.

```
/speckit.constitution Dessine moi une constitution
```

Cette commande va vous créer un fichier `.specify/memory/constitution.md` que vous allez pouvoir modifier vous même ou avec l'aide de votre IA préférée.

### Specifications fonctionnelles ###

On s'attaque aux spécifications. Expliquez clairement ce que vous voulez et pourquoi. Attention, rien de technique ici, que du fonctionnel.

```
/speckit.specify Développe une moulinette qui créée un site statique à partir de fichiers markdown etc.
```

L'IA va alors vous créer un fichier `specs/001-xxx/spec.md` qui va contenir la liste de toutes les spécifications de votre application. Encore une fois, à vous de relire, modifier et compléter.

### Clarification ###

Cette étape est là pour vérifier que les spécifications sont assez claires et exaustives. Si vous avez été un peu fénéant, c'est là que ca va se voir !

```
/speckit.clarify
```

L'IA va alors vous montrer tout ce que vous avez pu rater. Les trous dans la raquette comme on dit. Restera alors à compléter/corriger tout ce qui ne va pas. Ne négligez pas cette étape, plus vous serez précis, plus la suite sera facile. Comme à chaque étape, n'hésitez pas à challenger l'IA, vous pouvez relancer ce prompt plusieurs fois et vous pouvez même lui demander de se concentrer sur un sous ensemble de "votre" oeuvre.

### Plan ###

Tout est prêt, enfin presque, c'est le moment de lui en dire plus techniquement ! Vous allez pouvoir préciser le ou les langages de votre choix, le type de base de données et éventuellement la structure du projet.

```
/speckit.plan Nous allons utiliser du C# (.NET10) pour ce projet, pas de librairie moderne pour le front, simplement du html/css/js. Le projet de test doit utiliser xunit. Pour parser les fichiers markdown, utilise la librairie...
```

Cette étape va générer plusieurs fichiers en fonction de votre besoin. Encore une fois, il est important de tout relire et corriger si besoin.

### Taches ###

Maintenant tout devrait être clair et bien défini. Le travail à réaliser pouvant être important, il reste une dernière étape préparatoire pour aider l'IA à garder le cap lors de l'implémentation. Nous allons générer une liste de taches à réaliser les unes après les autres ou en parallèle.

```
/speckit.tasks
```

Ce prompt va créer le fichier `tasks.md`. Tout le travail à réaliser devrait être organisé en phases et en user stories. Il vous faut encore un peu de courage pour le relire et vérifier qu'il n'y a pas d'incohérence.

### Implémentation

Ca y est, il est temps de laisser l'IA travailler et d'aller boire un café bien mérité.

```
/speckit.implement
```

Ne vous éloignez pas trop, l'IA va probablement souvent vous demander l'autorisation d'executer certaines commandes.

### Ca marche... presque... ###

Une fois le travail de votre IA terminé, vous devriez vous retrouver avec un projet quasi terminé. Il y a fort à parier que tout ne sera pas parfait, vous allez surement devoir vibe coder certains correctifs mais sur l'ensemble de l'implémentation, vous devriez avoir gagner pas mal de temps !

### Conclusion ###

Ce que je retiens de cette expérience:
- J'ai gagné pas mal de temps (je ne l'aurais probablement jamais fait si j'avais dû le faire à la main).
- C'était formateur d'utiliser Spec Kit et de découvrir ses entrailles. Les prompts et les scripts sont lisibles et disponibles directement lors de l'installation !
- J'ai dû corriger/ajouter des features en vibe codant.
- Je n'aurai pas du tout fait comme ça si j'avais du le faire moi même (spec technique pas assez claire ?). Par exemple j'ai beaucoup de classes static. Pour un projet perso, je m'en contenterais pour le moment.
- Parfait pour démarrer un projet quand a l'idée mais pas réfléchit à tous les détails.
- Je prévois d'ajouter d'autres feature mais ce sera en vibe codant et plus avec Spec Kit.
