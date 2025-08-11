<script setup>

  defineProps({
    modalId: { type: String, required: true, },
    modalHeader: { type: String, required: false, },
  });


</script>


<template>
  <div class="modal fade mt-5" :id="modalId" tabindex="-1" :aria-labelledby="`${modalId}Label`"
    aria-hidden="true">
    <div class="modal-dialog modal-xl">
      <div class="modal-content">
        <div class="modal-header">
          <h1 class="modal-title fs-3 open-sans-font mt-3 ms-3" :id="`${modalId}Label`">
            {{ modalHeader }}
          </h1>
          <button type="button" class="btn-close pt-0 mb-4" data-bs-dismiss="modal" aria-label="Close" title="close 'Create Keep' page"></button>
        </div>
        <div class="modal-body">
          <slot></slot>
        </div>
      </div>
    </div>
  </div>
</template>


<style lang="scss" scoped>

.open-sans-font {
  font-weight: 600;
}

.modal {
  color: rgb(102, 100, 100);
}

.modal-header {
  border: none;
}

.modal-content {
  background-color: #f9f6fa;
}

.btn-close {
  font-size: 14px;
  --bs-btn-close-opacity: 1;
  --bs-btn-close-hover-opacity: 0.5;
  transition: all .2s ease-in-out;
}

</style>