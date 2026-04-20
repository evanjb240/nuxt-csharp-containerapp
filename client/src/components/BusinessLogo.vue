<template>
    <div :class="notCentered? '' : 'centered'">
        <img :src="getImageUrl()" :width="width" :height="height" />
    </div>
</template>
<script setup lang="ts">
const props = defineProps({
    width:{
        type: Number,
        default: 500
    },
    height:{
        type: Number,
        default: 231
    },
    color:{
        type: String,
        default: 'black'
    },
    notCentered:{
        type: Boolean,
        default: false
    }
});
const getImageUrl = () => {
    return new URL(`../assets/images/sample_logo_${props.color}.png`, import.meta.url).href;
}
</script>
<style scoped>
@media screen and (max-width: 600px){
    img{
        max-width: 250px;
        max-height: 115px;
    }
}
</style>