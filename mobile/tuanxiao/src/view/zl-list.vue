<template>
    <div>
        <div class="search-header">
            <div>
                <input type="text" placeholder="请输入文件名称">
                <span>搜索</span>
            </div>
        </div>
        <div class="zl-list-sec back-gray">
            <div class="zl-list-content">
                <router-link :to="{name:'zlDetail',params:{zlId:123}}" class="zl-list-model" tag="div">
                 <!--    <div class="zl-list-left back-cover" style="background-image: url('../assets/zl_video.png')"></div> -->
                 <img class="zl-list-left" src="../assets/zl_video.png" />
                    <div class="zl-list-right">
                        <div class="teacher-list-title text-one">时尚礼仪培训时尚礼仪培训时尚礼仪培训时尚礼仪培训</div>
                        <div><span class="zl-list-tag tag1">音频资料</span></div>
                        <div class="zl-list-date">发布时间：2016-02-03</div>
                    </div>
                </router-link>
            </div>
        </div>
        <Footer-Nav></Footer-Nav>
    </div>
</template>
<script>
//引入
import FooterNav from '../components/Footer'

export default {
    name: 'zlList',
    created() {
        setTitle('团校资讯');
    },
    components: {
        //注册组件
        FooterNav
    }
}
</script>
