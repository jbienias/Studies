class String
  def insert_multiple(values)
    self.each_char.with_index.inject('') do |memo, (c,i)|
      memo + values.fetch(i,'') + c
    end
  end
end
