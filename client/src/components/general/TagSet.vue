<template>
    <div class="tag-container">
        <div class="tag" :style="{ backgroundColor: tag.color}" v-for="tag in tags">
            {{tag.text}}
        </div>
    </div>
</template>

<script setup>

const props = defineProps({
    tags: { type: Array, default: Array<Tag>{} }
})

</script>

<script>
    export class Tag {
        constructor({text, color}){
            this.text = text;
            this.color = color;
        }
    }
</script>

<style>

.tag-container{
    display: flex;
    flex-direction: row;
    flex-wrap:wrap;
}

.tag{
  padding: 5px;
  font-size: 14px;
  margin:5px;
  border-radius: 2px;
}

.active, .accordion:hover {
  background-color: #ccc; 
}

.sub-header{
  font-size:14px;
}
</style>