<template>
    <div>
        <iframe class="zl-detail-video" src="http://v.youku.com/v_show/id_XMTkwNzUyNzc4OA==.html?spm=a2hww.20023042.m_223465.5~5~5~5!2~5~5~A.RdZmMC&f=29117919&from=y1.3-idx-beta-1519-23042.223465.4-1"></iframe>
        <div class="zl-detail-content">
            <div class="zl-list-model">
                <div class="zl-detail-title">给职场新人的EXCEL课</div>
            </div>
            <div class="zl-list-model" style="padding:1.625rem 0;width:100%;">
                <div class="zl-detail-text">优酷是中国领先的视频分享网站，由古永锵在2006年6月21日创立。优酷网以 “快者为王”为产品理念，注重用户体验，不断完善服务策略，其卓尔不群的“快速播放，快速发布，快速搜索”的产品特性，充分满足用户日益增长的多元化互动需求，使之.</div>
            </div>
            <div class="zl-list-model">
                <div class="zl-detail-title">相关资料
                    <div style="float: right;display: inline-block;text-align: right;">
                        <span class="zl-about-tag active"></span>
                        <span class="zl-about-tag"></span>
                    </div>
                </div>
                <div class="dt_pc_container">
                    <div id="dt_pic_div">
                        <div class="dt_pic_show">
                            <div class="back-cover" style="background-image:url('../../static/imgTest/zl_video.png');height:6rem;"></div>
                            <div class="dt_pic_text">
                                资料名称资料名称资料名称资料名称资料名称资料名称资料名称
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
export default {
    name: 'zlDetail',
    created() {
        // 组件创建结束后
        console.log(this.$route.params);
        setTitle('资料中心');
    }
}
</script>
