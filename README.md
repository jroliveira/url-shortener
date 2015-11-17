# URL Shortener

[![Build status](https://ci.appveyor.com/api/projects/status/ufy2x69giu07g27j?svg=true)](https://ci.appveyor.com/project/junioro/url-shortener)
[![Build Status](https://travis-ci.org/jroliveira/url-shortener.svg?branch=master)](https://travis-ci.org/jroliveira/url-shortener)

## O que é?

É um encurtador de url feito em C#.  
A aplicação roda em Mono e .NET Framework. 

## Tecnologias utilizadas 

Abaixo os principais pacotes/tecnologias utilizados em cada projeto.

### WebApi

 - Simple.Data
 - FluentMigrator
 - Nancy

### WebApi.Test

 - NUnit
 - Moq
 - Fluent Assertions 
 - Nancy.Testing

### Swagger docs

 - Nodejs
 - json-refs
 - yaml-js

## Rodar a aplicação

* `git clone https://github.com/jroliveira/url-shortener.git`
* `bundler`

## Gerar a documentação do Swagger

* `cd src/docs/UrlShortener.WebApi/docs`
* `npm install`
* `npm start`

## Rodar os testes

* `rake test`

## Contributions 

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