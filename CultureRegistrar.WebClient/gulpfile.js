var configLocal = require('./gulp-config-local');

var gulp = require('gulp');
var gulpWebpack = require('gulp-webpack');

gulp.task('update-local', [
    'update-local-app', 
    'update-local-html', 
    'update-local-js', 
    'update-local-libs'
]);

gulp.task('update-local-app', function() {
    return gulp
        .src('webpack-entry.js')
        .pipe(gulpWebpack({
            output: {
                filename: 'culture-registrar.bundle.js'
            }
        }))
        .pipe(gulp.dest(configLocal.webroot + '/static/js/'));
});

gulp.task('update-local-html', function() { 
    return gulp
        .src('./src/index.html')
        .pipe(gulp.dest(configLocal.webroot));
});

gulp.task('update-local-js', function() { 
    return gulp
        .src('./src/static/js/**/*.*')
        .pipe(gulp.dest(configLocal.webroot + '/static/js'));
});

gulp.task('update-local-libs', function() { 
    return gulp
        .src('./src/static/libs/**/*.*')
        .pipe(gulp.dest(configLocal.webroot + '/static/libs'));
});

gulp.task('watch', function() {
    gulp.watch('./src/app/**/*', ['update-local-app']);
    gulp.watch('./src/index.html', ['update-local-html']);
    gulp.watch('./src/static/js/**/*', ['update-local-js']);
    gulp.watch('./src/static/libs/**/*', ['update-local-libs']);
});