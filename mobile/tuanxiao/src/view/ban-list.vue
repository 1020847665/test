<template>
    <div>
        <Header-nav></Header-nav>
        <div class="teacher-list-sec" style="padding-top:5rem;">
            <img src="../assets/banner.png" style="width:100%;margin-bottom: 1.875rem;">
            <div>
                <router-link :to="{name:'banDetail',params:{banId:123}}" class="teacher-list-model" tag="div">
                    <div style="display:table;margin:2.5rem 0;">
                        <div class="teacher-list-img back-cover" style="background-image: url('../../static/imgTest/teacher1.png');width:35%;"></div>
                        <div class="teacher-list-right" style="width:60%;">
                            <div class="teacher-list-title">青年创业指导培训班（第十一期）</div>
                            <div class="teacher-list-content" style="margin-top:1rem;">主讲课程：企业战略分析与规划；品牌推广、市场策划；营销组织框架、销售管理体系设计讲课程：企业战略分析与规划；品牌推广、市场策划；营销组织框架、销售管理体系设计</div>
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
    name: 'banList',
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
