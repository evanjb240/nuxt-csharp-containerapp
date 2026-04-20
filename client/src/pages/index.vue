<template>
    <div>
        <HeroImage height="500px" :image="backgroundImage" :links="linkGroup" :showLogo="true" />
    </div>
</template>
<style scoped>
.name-display{
    font-family: 'MoonTime';
    font-size:100px;
}
.date{
    font-family: 'Barlow C Light';
    font-size: 30px;
    margin-top:5px;
    margin-bottom:5px;
}
</style>
<script setup lang="ts">
import type { Link } from '~/types/Link';

const backgroundImage = `url('~/assets/images/coding_background.png')`;

const linkGroup = ref<Link[]>([
    { link: '/Projects', text: 'Projects'},
    { link: '/Resume', text: 'Resume'}
])
</script>