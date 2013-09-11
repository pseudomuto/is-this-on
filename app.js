App = Ember.Application.create();

DS.RESTAdapter.reopen({
  namespace: 'data'
})

App.Store = DS.Store.extend({
  revision: 12
});

App.Doc = DS.Model.extend({
  heading: DS.attr('string')
});

App.Router.map(function() {
  // put your routes here
  this.resource('docs', function() {
    this.resource('doc', { path: ':doc_id' });
  });
});

App.DocsRoute = Ember.Route.extend({  
  model: function() {
    return this.store.findAll('doc');
  }
});

App.DocRoute = Ember.Route.extend({
  model: function(params) {
    return docs[0].items.findBy('id', '1');
  }
});

var markdownConverter = new Showdown.converter();

Ember.Handlebars.helper('markdown', function(md) {
  return new Handlebars.SafeString(markdownConverter.makeHtml(md));
});