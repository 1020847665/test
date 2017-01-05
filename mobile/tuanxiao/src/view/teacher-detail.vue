<template>
    <div class="teacher-detail-sec">
        <img class="teacher-detail-img" src="../../static/imgTest/teacher1.png" />
        <div style="margin-top: 2.5rem ;">
            <span class="teacher-list-name">冯程</span>
            <span class="teacher-list-tag tag2">团校教师</span>
        </div>
        <div style="clear: both;"></div>
        <div class="teacher-list-title">国内时尚礼仪教育专家，国内著名时尚礼仪培训师
        </div>
        <div class="teacher-detail-btn"><img src="../assets/order_icon.png">去预约</div>
        <div class="teacher-detail-intro"> 教师介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容介绍内容</div>
    </div>
</template>
<script>
export default {
    name: 'teacherDetail',
    created() {
        // 组件创建结束后
        console.log(this.$route.params);
        setTitle('教师风采');
    }
}
</script>
