<template>
    <div class="justify-center">
        <span class="loader"></span>
    </div>
</template>
<script setup lang="ts">
interface Props{
    color?: string;
    secondaryColor?: string;
    size?: number;
}
const props = withDefaults(defineProps<Props>(),{
    color: 'gray',
    secondaryColor: '#002D04',
    size: 36
})
</script>
<style scoped>
    .loader {
        width: v-bind("props.size + 'px'");
        height: v-bind("props.size + 'px'");
        border-radius: 50%;
        position: relative;
        animation: rotate 1s linear infinite
      }
      .loader::before , .loader::after {
        content: "";
        box-sizing: border-box;
        position: absolute;
        inset: 0px;
        border-radius: 50%;
        border: 5px solid v-bind("props.color");
        animation: prixClipFix 2s linear infinite ;
      }
      .loader::after{
        border-color: v-bind("props.secondaryColor");
        animation: prixClipFix 2s linear infinite , rotate 0.5s linear infinite reverse;
        inset: 6px;
      }

      @keyframes rotate {
        0%   {transform: rotate(0deg)}
        100%   {transform: rotate(360deg)}
      }

      @keyframes prixClipFix {
          0%   {clip-path:polygon(50% 50%,0 0,0 0,0 0,0 0,0 0)}
          25%  {clip-path:polygon(50% 50%,0 0,100% 0,100% 0,100% 0,100% 0)}
          50%  {clip-path:polygon(50% 50%,0 0,100% 0,100% 100%,100% 100%,100% 100%)}
          75%  {clip-path:polygon(50% 50%,0 0,100% 0,100% 100%,0 100%,0 100%)}
          100% {clip-path:polygon(50% 50%,0 0,100% 0,100% 100%,0 100%,0 0)}
      }
</style>