# URL Shortener

[![Build status](https://ci.appveyor.com/api/projects/status/ufy2x69giu07g27j?svg=true)](https://ci.appveyor.com/project/junioro/url-shortener)
[![Build Status](https://travis-ci.org/jroliveira/url-shortener.svg?branch=master)](https://travis-ci.org/jroliveira/url-shortener)

## O que é?

É um encurtador de url feito em C#.  
A aplicação roda em Mono e .NET Framework.  

Para desenvolvimento da aplicação foram utilizadas principalmente as bibliotecas.

 - Simple.Data
 - FluentMigrator
 - Nancy

Para os testes unitários foram usadas as bibliotecas.

 - NUnit
 - Moq
 - Fluent Assertions 

Para gerar a documentação da Swagger foi utilizado Nodejs com as bibliotecas.

 - json-refs
 - yaml-js

## Partes da aplicação

### UrlShortener.WebApi

É o core do sistema e também é interface REST em Nancy como mencionado acima.  

#### Filtro

A interface REST possui um sistema de filtro baseado no projeto [StrongLoop Node.js API Platform][strongloop] desenvolvido pela [Atlassian Confluence][atlassian].

[**Limit**][limit]

``` js
/urls?filter[limit]=5
```

[**Skip**][skip]

``` js
/urls?filter[skip]=50
```

[**Order**][order]

``` js
/urls?filter[order]=address%desc

/urls?filter[order]=address%20asc
```

[**Where**][where]

``` js
/urls?filter[where][id][gt]=100

/urls?filter[where][id][lt]=100
```

### Rodar a aplicação

#### Rodar a API

* `git clone https://github.com/jroliveira/url-shortener.git`
* `bundler`

#### Gerar a documentação do Swagger

* cd docs
* npm install
* npm start

### Contributions 

1. Fork it
2. git checkout -b <branch-name>
3. git add --all && git commit -m "feature description"
4. git push origin <branch-name>
5. Create a pull request

[strongloop]: https://docs.strongloop.com/display/SL/Installing+StrongLoop
[atlassian]: https://www.atlassian.com/software/confluence
[limit]: https://docs.strongloop.com/display/public/LB/Limit+filter
[skip]: https://docs.strongloop.com/display/public/LB/Skip+filter
[order]: https://docs.strongloop.com/display/public/LB/Order+filter
[where]: https://docs.strongloop.com/display/public/LB/Where+filter