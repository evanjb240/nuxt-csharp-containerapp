<template>
    <div>
        <h1 class="centered">
            Projects
        </h1>
        <Expander
            header="About Us"
            sub-header="Some information about us."
            :index="1"
            @toggleAccordion="toggleProjectsAccordion"
            :isOpen="activeProjectsAccordion == 1"
            >
            <template #main>
              <div class="justify-center">
                <video width="75%" controls autoplay muted loop>
                </video>
              </div>
            </template>
        </Expander>
    </div>
</template>
<script setup lang="ts">
  import { ref } from 'vue';

  const activeProjectsAccordion = ref(1);
  
  function toggleProjectsAccordion(index: number){
      activeProjectsAccordion.value = activeProjectsAccordion.value == index? 0 : index;
  }
</script>
<style scoped>
.column-container{
    display:flex;
    justify-content: space-between;
}
.column{
    width:100%;
    display:flex;
    flex-direction: column;
}
.column-section{
    display: flex;
    flex-direction: column;
    align-items: flex-start;
}

.question{
    padding: 5px;
    font-family: 'Barlow B';
    font-size: 26px;
}
.answer{
    font-family: 'Barlow CR';
    padding: 5px;
}

.bottom-spacing{
    padding-bottom:50px;
}
</style>