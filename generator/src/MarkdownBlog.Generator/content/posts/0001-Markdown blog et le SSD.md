---
title:
  fr: "MarkdownBlog et le Spec-Driven Development"
  en: "MarkdownBlog and Spec-Driven Development"
date: 2020-01-01
excerpt:
  fr: "C'est quoi le Spec-Driven Development et comment ça m'a aidé à construire ce blog."
  en: "What is Spec-Driven Development and how it helped me build this blog."
cover: ../../assets/img/cover.svg
tags:
  - dotnet
  - MarkdownBlog
  - SDD
  - Spec-Driven Development
  - Spec Kit
---

Bienvenue sur mon blog !

Je voulais ouvrir un blog depuis des années, pour de nombreuses raisons.
La première, c'est simplement de vérifier mon niveau de compréhension. Si je parviens à expliquer un concept, c'est qu'il est clair dans mon esprit. Pour éviter d'écrire des âneries, je vais certainement approfondir mes connaissances. Enfin, dans un registre moins personnel, cela me permet de partager des découvertes et de provoquer des discussions. Cela m'aide également à élargir mon réseau et à collaborer davantage.

Très simplement, voici ce que je voulais :
- [Ne jamais perdre mes mots](https://www.hanselman.com/blog/your-words-are-wasted)
- Pouvoir écrire sans me soucier du HTML
- Ne pas héberger moi-même le blog
- Pouvoir changer d'hébergeur facilement
- Ne pas payer pour l'hébergement (si possible)

## MarkdownBlog ##

Ce qui m'a toujours freiné, c'était la mise en place du blog. Écrire, oui, mais créer le blog... Pas le temps, toujours mieux à faire, etc. Je suis sûr que c'est un sentiment assez répandu.

Pour mon plus grand bonheur, depuis quelque temps, l'IA fait de gros progrès et me permet maintenant de me lancer dans la construction d'un blog sans y passer énormément de temps. Cerise sur le gâteau : j'en ai profité pour explorer des outils d'IA générative.

Pour répondre à ce besoin, j'ai créé un projet sur mesure, **MarkdownBlog**, un générateur de blog statique en .NET 10 : page d’accueil, index des articles, pages d’articles, et les bases SEO comme les tags Open Graph, les URLs canoniques, `robots.txt` et un `sitemap.xml`. Ça parse des fichiers markdown pour générer un blog statique. Ce projet est hébergé [sur GitHub](https://github.com/TedevFR/MarkdownBlog) et une petite GitHub Action publie le résultat grâce à GitHub Pages.

Une fois le projet terminé, je n'aurais plus qu'à pousser un nouveau fichier markdown pour que le blog soit à jour. Parfait !

Il n'y a plus qu'à s'y mettre. Pour rendre ça amusant et formateur, j'ai décidé de le faire en Spec-Driven Development.

## Le Spec-Driven Development

Après le TDD et le BDD, le SDD (rien à voir avec un disque dur), le Spec-Driven Development.
L'idée, c'est d'écrire les spécifications en premier, puis de générer le code à partir de ça... C'est déjà ce qu'on fait, vous allez me dire, et l'outil, c'est le développeur. Alors, quelle est la différence ?
Le terme est encore nouveau, je vous conseille la lecture de [cet article](https://martinfowler.com/articles/exploring-gen-ai/sdd-3-tools.html) pour comprendre que le sens n'est pas encore complètement défini. Je vais tenter ici de vous donner ma propre définition.

L'approche SDD met encore plus en avant les spécifications en les faisant devenir ce qu'il y a de plus important dans le projet. Ce n'est plus le code qui est maintenu dans le repo mais bien les spécifications. Le code reste le résultat final, mais il perd de son importance. Il est devenu jetable pour de bon, comme vos binaires en local après avoir débuggué.
Tout ça est bien entendu possible dans un contexte plus ou moins futuriste dans lequel les humains n'écrivent plus de code mais que cette tâche est confiée à une IA.

Quel est l'intérêt ?

Comme régulièrement en programmation, à encapsuler un concept complexe pour nous permettre de nous concentrer sur un niveau d'abstraction supérieur. Si on regarde l'histoire, on a commencé avec de la programmation très bas niveau. Coucou l'assembleur. Tout était long, il fallait tout faire nous-même. Avec le temps, sont apparus des langages de plus haut niveau, permettant d'oublier certains détails et de se concentrer sur des choses plus importantes comme l'architecture. Le SDD prétend être l'étape suivante en utilisant directement le langage humain. L'architecture et la connaissance même d'un langage de programmation pourraient devenir des détails d'implémentation. C'est l'IA qui gérera tout ça.

Plus concrètement, vous décidez de changer le langage dans lequel est faite votre application ? Changez votre spec (oui, une spec peut aussi être technique !) et regénérez votre projet, c'est terminé. Bien sûr, l'IA d'aujourd'hui ne nous permet pas encore de faire ça, mais ce sera probablement possible un jour.

## Spec Kit ##

Je savais ce que je voulais mais seulement dans les grandes lignes. Je pensais qu'il allait falloir écrire toutes les spécifications de façon fastidieuse puis que Spec Kit allait faire tout le code à ma place. Pas du tout ! Spec Kit m'a permis de me lancer rapidement sans avoir tous les détails en tête. Spec Kit définit un workflow pour vous aider à générer tout ce dont il a besoin. Chaque étape est réalisée à l'aide de son propre prompt. Ces prompts sont installés lors de l'initialisation de Spec Kit. Je vais passer sur la phase d'installation mais tout est expliqué sur leur [GitHub](https://github.com/github/spec-kit). Regardons ensemble les étapes dans l'ordre.


### Constitution ###

La première chose à faire est d'établir la constitution, c'est-à-dire l'ensemble des règles concernant la qualité du code, les standards à respecter, les contraintes de performance, etc. Si vous avez le syndrome de la page blanche ou pas envie de trop vous embêter pour un projet sans enjeux, vous pouvez demander à l'IA de générer une première version pour vous. Dans mon cas, je lui ai simplement demandé de créer la constitution parfaite pour lui en expliquant très rapidement le principe de mon application.

```
/speckit.constitution Dessine-moi une constitution
```

Cette commande va vous créer un fichier `.specify/memory/constitution.md` que vous allez pouvoir modifier vous-même ou avec l'aide de votre IA préférée.

### Spécifications fonctionnelles ###

On s'attaque aux spécifications. Expliquez clairement ce que vous voulez et pourquoi. Attention, rien de technique ici, que du fonctionnel.

```
/speckit.specify Développe une moulinette qui crée un site statique à partir de fichiers markdown, etc.
```

L'IA va alors vous créer un fichier `specs/001-xxx/spec.md` qui va contenir la liste de toutes les spécifications de votre application. Encore une fois, à vous de relire, modifier et compléter.

### Clarification ###

Cette étape est là pour vérifier que les spécifications sont assez claires et exhaustives. Si vous avez été un peu fainéant, c'est là que ça va se voir !

```
/speckit.clarify
```

L'IA va alors vous montrer tout ce que vous avez pu rater. Les trous dans la raquette comme on dit. Restera alors à compléter/corriger tout ce qui ne va pas. Ne négligez pas cette étape, plus vous serez précis, plus la suite sera facile. Comme à chaque étape, n'hésitez pas à challenger l'IA, vous pouvez relancer ce prompt plusieurs fois et vous pouvez même lui demander de se concentrer sur un sous-ensemble de "votre" œuvre.

### Plan ###

Tout est prêt, enfin presque, c'est le moment de lui en dire plus techniquement ! Vous allez pouvoir préciser le ou les langages de votre choix, le type de base de données et éventuellement la structure du projet.

```
/speckit.plan Nous allons utiliser du C# (.NET10) pour ce projet, pas de librairie moderne pour le front, simplement du html/css/js. Le projet de test doit utiliser xunit. Pour parser les fichiers markdown, utilise la librairie...
```

Cette étape va générer plusieurs fichiers en fonction de votre besoin. Encore une fois, il est important de tout relire et corriger si besoin.

### Tâches ###

Maintenant tout devrait être clair et bien défini. Le travail à réaliser pouvant être important, il reste une dernière étape préparatoire pour aider l'IA à garder le cap lors de l'implémentation. Nous allons générer une liste de tâches à réaliser les unes après les autres ou en parallèle.

```
/speckit.tasks
```

Ce prompt va créer le fichier `tasks.md`. Tout le travail à réaliser devrait être organisé en phases et en user stories. Il vous faut encore un peu de courage pour le relire et vérifier qu'il n'y a pas d'incohérence.

### Implémentation

Ça y est, il est temps de laisser l'IA travailler et d'aller boire un café bien mérité.

```
/speckit.implement
```

Ne vous éloignez pas trop, l'IA va probablement souvent vous demander l'autorisation d'exécuter certaines commandes.

### Ça marche... presque... ###

Une fois le travail de votre IA terminé, vous devriez vous retrouver avec un projet quasi terminé. Il y a fort à parier que tout ne sera pas parfait, vous allez sûrement devoir vibe coder certains correctifs mais sur l'ensemble de l'implémentation, vous devriez avoir gagné pas mal de temps !

## Conclusion ##

Ce que je retiens de cette expérience :
- J'ai gagné pas mal de temps (je ne l'aurais probablement jamais fait si j'avais dû le faire à la main).
- C'était formateur d'utiliser Spec Kit et de découvrir ses entrailles. Les prompts et les scripts sont lisibles et disponibles directement lors de l'installation !
- J'ai dû corriger/ajouter des features en vibe codant.
- Je n'aurais pas du tout fait comme ça si j'avais dû le faire moi-même (spec technique pas assez claire ?). Par exemple, j'ai beaucoup de classes statiques. Pour un projet perso, je m'en contenterai pour le moment.
- Parfait pour démarrer un projet quand on a l'idée mais pas encore tous les détails.
- Je prévois d'ajouter d'autres fonctionnalités mais ce sera en vibe codant et plus avec Spec Kit.

<!-- lang:en -->

Welcome to my blog!

I've wanted to start a blog for years, for many reasons.
The first is simply to verify my level of understanding. If I can explain a concept, it's clear in my mind. To avoid writing nonsense, I will certainly deepen my knowledge. Finally, on a less personal note, it allows me to share discoveries and spark discussions. It also helps me expand my network and collaborate more.

Very simply, here's what I wanted:
- [Never lose my words](https://www.hanselman.com/blog/your-words-are-wasted)
- Be able to write without worrying about HTML
- Not host the blog myself
- Be able to change hosting easily
- Not pay for hosting (if possible)

## MarkdownBlog ##

What always held me back was setting up the blog. Writing, yes, but creating the blog... No time, always something better to do, etc. I'm sure this is a fairly common feeling.

To my great delight, AI has made significant progress recently and now allows me to start building a blog without spending a huge amount of time. Cherry on top: I took the opportunity to explore generative AI tools.

To meet this need, I created a custom project, **MarkdownBlog**, a static blog generator in .NET 10: home page, article index, article pages, and SEO basics like Open Graph tags, canonical URLs, `robots.txt` and a `sitemap.xml`. It parses markdown files to generate a static blog. This project is hosted [on GitHub](https://github.com/TedevFR/MarkdownBlog) and a small GitHub Action publishes the result using GitHub Pages.

Once the project is finished, I'll just need to push a new markdown file to update the blog. Perfect!

Now it's time to get started. To make it fun and educational, I decided to do it with Spec-Driven Development.

## Spec-Driven Development

After TDD and BDD, comes SDD (nothing to do with a hard drive), Spec-Driven Development.
The idea is to write specifications first, then generate code from them... That's already what we do, you might say, and the tool is the developer. So what's the difference?
The term is still new, I recommend reading [this article](https://martinfowler.com/articles/exploring-gen-ai/sdd-3-tools.html) to understand that the meaning is not yet fully defined. I will try here to give you my own definition.

The SDD approach emphasizes specifications even more by making them the most important thing in the project. It's no longer the code that is maintained in the repo but the specifications. The code remains the final result, but it loses its importance. It has become truly disposable, like your local binaries after debugging.
All this is of course possible in a more or less futuristic context in which humans no longer write code but this task is entrusted to an AI.

What's the point?

As often in programming, to encapsulate a complex concept to allow us to focus on a higher level of abstraction. If we look at history, we started with very low-level programming. Hello assembler. Everything was long, we had to do everything ourselves. Over time, higher-level languages appeared, allowing us to forget certain details and focus on more important things like architecture. SDD claims to be the next step by directly using human language. Architecture and even knowledge of a programming language could become implementation details. AI will handle all that.

More concretely, you decide to change the language in which your application is made? Change your spec (yes, a spec can also be technical!) and regenerate your project, that's it. Of course, today's AI doesn't yet allow us to do that, but it will probably be possible one day.

## Spec Kit ##

I knew what I wanted but only in broad strokes. I thought I would have to write all the specifications tediously and then Spec Kit would do all the code for me. Not at all! Spec Kit allowed me to get started quickly without having all the details in mind. Spec Kit defines a workflow to help you generate everything it needs. Each step is done using its own prompt. These prompts are installed when initializing Spec Kit. I'll skip the installation phase but everything is explained on their [GitHub](https://github.com/github/spec-kit). Let's look at the steps in order.

### Constitution ###

The first thing to do is establish the constitution, that is, all the rules regarding code quality, standards to follow, performance constraints, etc. If you have writer's block or don't want to bother too much for a low-stakes project, you can ask the AI to generate a first version for you. In my case, I simply asked it to create the perfect constitution for it by briefly explaining the principle of my application.

```
/speckit.constitution Draw me a constitution
```

This command will create a `.specify/memory/constitution.md` file that you can modify yourself or with the help of your favorite AI.

### Functional Specifications ###

Let's tackle the specifications. Clearly explain what you want and why. Be careful, nothing technical here, only functional.

```
/speckit.specify Develop a tool that creates a static site from markdown files, etc.
```

The AI will then create a `specs/001-xxx/spec.md` file that will contain the list of all your application's specifications. Again, it's up to you to review, modify and complete.

### Clarification ###

This step is there to verify that the specifications are clear and exhaustive enough. If you've been a bit lazy, this is where it will show!

```
/speckit.clarify
```

The AI will then show you everything you may have missed. The holes in the racket, as we say. All that's left is to complete/correct everything that's wrong. Don't neglect this step, the more precise you are, the easier the rest will be. As with each step, don't hesitate to challenge the AI, you can run this prompt several times and you can even ask it to focus on a subset of "your" work.

### Plan ###

Everything is ready, well almost, it's time to tell it more technically! You will be able to specify the language(s) of your choice, the type of database and possibly the project structure.

```
/speckit.plan We will use C# (.NET10) for this project, no modern library for the front, just html/css/js. The test project should use xunit. To parse markdown files, use the library...
```

This step will generate several files depending on your needs. Again, it's important to review everything and correct if necessary.

### Tasks ###

Now everything should be clear and well defined. As the work to be done can be significant, there is one last preparatory step to help the AI stay on track during implementation. We will generate a list of tasks to be performed one after another or in parallel.

```
/speckit.tasks
```

This prompt will create the `tasks.md` file. All the work to be done should be organized into phases and user stories. You still need a little courage to review it and check that there are no inconsistencies.

### Implementation

That's it, it's time to let the AI work and go have a well-deserved coffee.

```
/speckit.implement
```

Don't go too far, the AI will probably often ask you for permission to execute certain commands.

### It works... almost... ###

Once your AI's work is finished, you should end up with an almost finished project. There's a good chance that not everything will be perfect, you'll probably have to vibe code some fixes but overall, you should have saved a lot of time!

## Conclusion ##

What I take away from this experience:
- I saved a lot of time (I probably would never have done it if I had to do it by hand).
- It was educational to use Spec Kit and discover its internals. The prompts and scripts are readable and available directly upon installation!
- I had to fix/add features by vibe coding.
- I wouldn't have done it at all like this if I had to do it myself (technical spec not clear enough?). For example, I have a lot of static classes. For a personal project, I'll settle for that for now.
- Perfect for starting a project when you have the idea but not all the details yet.
- I plan to add more features but it will be by vibe coding and no longer with Spec Kit.
