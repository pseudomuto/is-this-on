App = Ember.Application.create();

var docs = [{ 
  heading: 'Configuration',
  items: [
    { 
      id: '1', 
      title: 'Simple Setup', 
      body: '## Body'
    }
  ]
}, {
  heading: 'Switch Providers',
  items: [
    {
      id: '2',
      title: 'ConfigSwitchProvider',
      body: ''
    },
    {
      id: '3',
      title: 'JsonSwitchProvider',
      body: ''
    }
  ]
}, {
  heading: 'Storage Providers',
  items: [
    {
      id: '4',
      title: 'NullStorageProvider',
      body: ''
    },
    {
      id: '5',
      title: 'CookieStorageProvider',
      body: ''
    }
  ]
}];

App.Router.map(function() {
  // put your routes here
  this.resource('docs', function() {
    this.resource('doc', { path: ':doc_id' });
  });
});

App.DocsRoute = Ember.Route.extend({
  model: function() {
    return docs;
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