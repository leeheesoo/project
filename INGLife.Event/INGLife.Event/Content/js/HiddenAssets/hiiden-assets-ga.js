function gaHiddenAsset(category, action, callback) {
    ga('send', 'event', category, action);
    callback();
};