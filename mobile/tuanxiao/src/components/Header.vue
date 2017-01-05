<template>
    <header>
        <router-link to="/ban-list" tag="div"><span>培训班次<span class="header-line"></span></span>
        </router-link>
        <router-link to="/teacher-list" tag="div"><span>团校课程<span class="header-line"></span></span>
        </router-link>
        <router-link to="/teacher-list" class="active" tag="div"><span>教师风采<span class="header-line"></span></span>
        </router-link>
    </header>
</template>
<script>
export default {
    name: 'header'
}
</script>
