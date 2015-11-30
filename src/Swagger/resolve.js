'use strict';

const
  fs = require('fs'),
  yaml = require('yaml-js'),
  jsonRefs = require('json-refs');

let swaggerYaml = fs.readFileSync('swagger.yaml').toString();
let root = yaml.load(swaggerYaml);

let options = {
  processContent: process
};

jsonRefs
  .resolveRefs(root, options)
  .then(done);

function process(content) {
  return yaml.load(content);
}

function done(results) {
  let json = JSON.stringify(results.resolved, null, 2);

  fs.writeFileSync('../UrlShortener.WebApi/swagger.json', json, 'utf8');
}