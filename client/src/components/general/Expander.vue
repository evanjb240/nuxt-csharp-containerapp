<template>
    <button class="accordion" :class="isOpen? 'opened': 'closed' " @click="toggleAccordion()">
        <div class="accordion-header">
          <div>
            <span>{{`${header ?? ''}${headerDecoration? ' | ': ''}`}}<i>{{ `${headerDecoration ?? ''}` }}</i></span>
          </div>
          <div>
            <span class="sub-header">{{ subHeader }}</span>
          </div>
        </div>
        <plus-icon size="20" v-show="!isOpen"></plus-icon>
        <minus-icon size="20" v-show="isOpen"></minus-icon>
    </button>
    <div class="accordion-content" v-show="isOpen">
      <slot name="main"></slot>
    </div>
</template>

<script setup lang="ts">

import { PlusIcon } from '@zhuowenli/vue-feather-icons'
import { MinusIcon } from '@zhuowenli/vue-feather-icons'

const props = defineProps({
    header: { type: String, default: '' },
    headerDecoration: { type: String, default: '' },
    subHeader:{ type:String, default: ''},
    index: {type:Number, default: 1},
    isOpen: { type:Boolean, default: ''},
    toggleAccordion: { type:Function }
})

const emit = defineEmits(['toggleAccordion']);

function toggleAccordion(){
  emit('toggleAccordion', props.index);
}


</script>

<style>
.accordion {
  display:flex;
  justify-content: space-between;
  align-items: center;
  background-color: rgb(210, 210, 210);
  color: #444;
  cursor: pointer;
  margin-top: 10px;
  padding: 10px;
  width: 100%;
  border: none;
  text-align: left;
  outline: none;
  font-size: 18px;
  transition: 0.4s;
  font-weight: 600;
  font-family: 'Barlow CR';

  &.opened{
    border-radius: 5px 5px 0px 0px;
  }
  &.closed{
    border-radius: 5px 5px 5px 5px;
  }
}

.accordion-header{
    display: flex;
    flex-direction: column;
}

.accordion-content{
  border: 1px;
  border-style: solid;
  border-color:lightgray;
  padding:20px;
}

.active, .accordion:hover {
  background-color: #ccc; 
}

.sub-header{
  font-size:14px;
}
</style>