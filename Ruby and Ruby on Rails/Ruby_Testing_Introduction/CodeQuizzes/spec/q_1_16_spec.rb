require 'q_1_16'
#HARD MODE, TO DO
describe "#insert_multiple" do
  it "inserts multiple strings" do
    input = { 3=>"<b>", 6=>"</b>" }
    str = "aaabbbccc"
    expected = "aaa<b>bbb</b>ccc"
    expect(str.insert_multiple(input)).to eq expected
  end
end
