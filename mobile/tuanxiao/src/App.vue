<template>
    <div id="app">
        <router-view></router-view>
    </div>
</template>
<script>
//引入
import HeaderNav from './components/Header'
import FooterNav from './components/Footer'

export default {
    name: 'app',
    components: {
        //注册组件
        HeaderNav,
        FooterNav
    }
}

</script>
