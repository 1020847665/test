// 载入外挂
var gulp = require('gulp'),
    runSequence = require('run-sequence'),
    sass = require('gulp-ruby-sass'),
    autoprefixer = require('gulp-autoprefixer'),
    minifycss = require('gulp-minify-css'),
    jshint = require('gulp-jshint'),
    uglify = require('gulp-uglify'),
    imagemin = require('gulp-imagemin'),
    rename = require('gulp-rename'),
    clean = require('gulp-clean'),
    concat = require('gulp-concat'),
    notify = require('gulp-notify'),
    cache = require('gulp-cache'),
    connect = require('gulp-connect'),
    replace = require('gulp-replace'),
    htmlmin = require('gulp-htmlmin'),
    livereload = require('gulp-livereload'),
    stripDebug = require('gulp-strip-debug'),
    rev = require('gulp-rev'),
    //- 对文件名加MD5后缀
    revCollector = require('gulp-rev-collector');
//- 路径替换;

// 样式
gulp.task('styles', function() {
    return gulp.src('app/css/*.css')
        .pipe(autoprefixer('last 2 version', 'safari 5', 'ie 8', 'ie 9', 'opera 12.1', 'ios 6', 'android 4'))
        .pipe(concat('index.css'))
        .pipe(gulp.dest('dist/css'))
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(minifycss())
        .pipe(rev())
        .pipe(gulp.dest('dist/css'))
        .pipe(rev.manifest()) //- 生成一个rev-manifest.json   //- 文件名加MD5后缀
        .pipe(gulp.dest('dist/rev/css'));
});

gulp.task('rev', function() {
    gulp.src(['dist/rev/**/*.json', 'dist/test.html']) //- 读取 rev-manifest.json 文件以及需要进行css名替换的文件
        .pipe(revCollector()) //- 执行文件内css名的替换
        .pipe(gulp.dest('dist/')); //- 替换后的文件输出的目录
});

// 脚本
gulp.task('scripts-serve', function() {
    return gulp.src('app/scripts/**/*.js')
        .pipe(concat('all.js'))
        .pipe(gulp.dest('dist/scripts'))
        //清除console
        .pipe(stripDebug())
        .pipe(rename({
            suffix: '.min'
        }))
        .pipe(uglify())
        .pipe(rev())
        .pipe(gulp.dest('dist/scripts/'))
        .pipe(rev.manifest())
        .pipe(gulp.dest('dist/rev/scripts/'));
});
// 脚本
gulp.task('scripts', function() {
    return gulp.src('app/scripts/**/*.js')
        .pipe(gulp.dest('dist/scripts'));

});
gulp.task('plugins',function(){
	    return gulp.src('app/plugins/**')
        .pipe(gulp.dest('dist/plugins'));
    });

// 图片
gulp.task('images', function() {
    return gulp.src('app/imgs/**/**')
        .pipe(cache(imagemin({
            optimizationLevel: 3,
            progressive: true,
            interlaced: true
        })))
        .pipe(gulp.dest('dist/imgs'));
});
//font
gulp.task('font', function() {
    return gulp.src('app/font/**/**')
        .pipe(gulp.dest('dist/font'));
});
//html
gulp.task('views', function() {
    return gulp.src("app/**/**/**/*.html")
        .pipe(htmlmin({
            removeComments: true, //清除HTML注释
            collapseWhitespace: true, //压缩HTML
            collapseBooleanAttributes: true, //省略布尔属性的值 <input checked="true"/> ==> <input />
            removeEmptyAttributes: true, //删除所有空格作属性值 <input id="" /> ==> <input />
            removeScriptTypeAttributes: true, //删除<script>的type="text/javascript"
            removeStyleLinkTypeAttributes: true, //删除<style>和<link>的type="text/css"
            minifyJS: true, //压缩页面JS
            minifyCSS: true //压缩页面CSS
        }))
        .pipe(revCollector())
        .pipe(gulp.dest("dist/"));
});

//bower
gulp.task('bower', function() {
    return gulp.src("bower_components/**/**/**/**")
        .pipe(gulp.dest("dist/bower_components"));
});


// 清理
gulp.task('clean', function() {
    return gulp.src(['dist/css', 'dist/scripts', 'dist/imgs', 'dist/font', 'dist/views', 'dist/rev', 'dist/*.html'], {
            read: false
        })
        .pipe(clean());
});

// 预设任务
gulp.task('default', ['clean'], function() {
    gulp.start('styles', 'scripts', 'images', 'views', 'font');
});

// 看守
gulp.task('watch', function() {

    // 看守所有.scss档
    gulp.watch('app/css/**/*.css', ['styles']);

    // 看守所有.js档
    gulp.watch('app/scripts/**/*.js', ['scripts']);

    // 看守所有图片档
    gulp.watch('app/imgs/**/*', ['images']);
    //看守所有字体
    gulp.watch('app/font/**', ['font']);

    // 看守所有的页面
    gulp.watch('app/**/**/**/**.html', ['views']);

    // 建立即时重整伺服器
    var webServer = livereload();

    // 看守所有位在 dist/  目录下的档案，一旦有更动，便进行重整
    gulp.watch(['dist/**']).on('change', function(file) {
        livereload.reload("dist/index.html");
    });

});


gulp.task('build', function(done) {
    runSequence(['images'], ['font'], ['bower'], ['scripts'], ['styles'],['plugins'], ['views'], ['rev'], done);
});

//发布-服务器正式版本
gulp.task('build-server', ['scripts-serve', 'styles', 'plugins','images', 'views', 'bower', "font",'rev'], function() {});
// 启动服务
gulp.task('serve', ['build', 'watch'], function() {
    connect.server({
        root: 'dist',
        prot: '8080',
        livereload: true
    });
});

gulp.task('networkServe', ['build-server', 'watch'], function() {
    connect.server({
        root: 'dist',
        prot: '8080',
        livereload: true
    });
});
