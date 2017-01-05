<template>
        <footer>
            <router-link to="/ban-list" class="active" tag="div">
                <div>
                    <!-- <img src="../assets/footer_icon1.png"> -->
                    <img src="../assets/footer_icon1_s.png">
                </div>
                <div>团校资讯</div>
            </router-link>
            <router-link to="/ban-list" class="active" tag="div">
                <div>
                    <img src="../assets/footer_icon2.png">
                    <!-- <img src="../assets/footer_icon2_s.png"> -->
                </div>
                <div>定制培训</div>
            </router-link>
            <router-link to="/zl-list" class="active" tag="div">
                <div>
                    <img src="../assets/footer_icon3.png">
                    <!-- <img src="../assets/footer_icon3_s.png"> -->
                </div>
                <div>资料中心</div>
            </router-link>
            <router-link to="/teacher-list" class="active" tag="div">
                <div>
                    <img src="../assets/footer_icon4.png">
                    <!-- <img src="../assets/footer_icon4_s.png"> -->
                </div>
                <div>个人中心</div>
            </router-link>
        </footer>
</template>
<script>
export default {
    name: 'footer'
}
</script>
