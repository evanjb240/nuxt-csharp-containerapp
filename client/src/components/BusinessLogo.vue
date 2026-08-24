<template>
    <div class="logo" :class="notCentered? '' : 'centered'">
        <img :src="getImageUrl()"  />
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
.logo{
    width: v-bind("props.width + 'px'");
    height: v-bind("props.height + 'px'");
}
.logo img {
  display: block;
  width: 100%;
  height: 100%;
  max-width: 100%;
  max-height: 100%;
  object-fit: contain;
  object-position: center;
}
@media screen and (max-width: 600px){
    .logo img {
        max-width: 250px;
        max-height: 115px;
    }
}
</style>