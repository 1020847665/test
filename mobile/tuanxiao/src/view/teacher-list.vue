<template>
    <div>
        <Header-nav></Header-nav>
        <div class="teacher-list-sec">
            <div>
                <router-link :to="{name:'teacherDetail',params:{teacherId:123}}" class="teacher-list-model" tag="div">
                    <div style="margin-top: 1.875rem;">
                        <div class="teacher-list-name">冯程</div>
                        <div class="teacher-list-tag tag2">团校教师</div>
                    </div>
                    <div style="clear: both;"></div>
                    <div style="display:table;margin:1.25rem 0 3.75rem;">
                        <div class="teacher-list-img back-cover" style="background-image: url('../../static/imgTest/teacher1.png')"></div>
                        <div class="teacher-list-right">
                            <div class="teacher-list-title">国内时尚礼仪教育专家，国内著名时尚礼仪培训师</div>
                            <div class="teacher-list-content">主讲课程：企业战略分析与规划；品牌推广、市场策划；营销组织框架、销售管理体系设计</div>
                        </div>
                    </div>
                </router-link>
            </div>
        </div>
        <Footer-Nav></Footer-Nav>
    </div>
</template>
<script>
//引入
import HeaderNav from '../components/Header'
import FooterNav from '../components/Footer'

export default {
    name: 'teacherList',
    created() {
        setTitle('团校资讯');
    },
    components: {
        //注册组件
        HeaderNav,
        FooterNav
    }
}
</script>
